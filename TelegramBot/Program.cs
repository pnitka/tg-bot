using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class Program
{
    static ITelegramBotClient? botClient;

    static async Task Main() 
    {
        botClient = new TelegramBotClient("7341322003:AAFI9kiivat_xkD3NS2l4wkL1PrIa9GFFVc");

        var cts = new CancellationTokenSource();
        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Бот запущен: @{me.Username}");

        // Начинаем получать обновления
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions { AllowedUpdates = { } }, // Получаем все обновления
            cancellationToken: cts.Token
        );

        Console.WriteLine("Нажмите любую клавишу для остановки работы бота...");
        Console.ReadLine();
        
        // Остановка получения обновлений
        cts.Cancel();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) 
    {
        if (update.Type == UpdateType.Message && update.Message.Text != null) 
        {
            switch (update.Message.Text.ToLower()) 
            {
                case "/start":
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Добро пожаловать в Telegram-бота!", cancellationToken: cancellationToken);
                    break;
                // Добавьте другие команды здесь
                default:
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Неизвестная команда. Попробуйте /start.", cancellationToken: cancellationToken);
                    break;
            }
        }
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Произошла ошибка: {exception.Message}");
        return Task.CompletedTask;
    }
}