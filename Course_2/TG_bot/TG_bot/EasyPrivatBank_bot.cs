using System;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;


namespace TG_bot
{
    public class EasyPrivatBank_bot
    {
        TelegramBotClient botClient = new TelegramBotClient("5427717344:AAESHYHqG70UV9t11M5jZNut5gSmjmTZqow");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Бот {botMe.Username} почав працювати!");
            Console.ReadKey();

        }

        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка в телеграм бот АПІ: \n{apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message.Text != null)
            {
                await HandlerMessageAsync(botClient, update.Message);
            }
        }

        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {

                ReplyKeyboardMarkup replyKeyboard = new
                    (
                        new[]
                        {
                            new KeyboardButton[] { "Курс валют", "Власний архів" },
                            new KeyboardButton[] { "Знайти відділення", "Знайти термінал" },
                        }

                    )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть пункт меню:", replyMarkup: replyKeyboard);
                return;
            } else
            if (message.Text == "Курс валют")
            {
                InlineKeyboardMarkup KeyboardMarkup = new
                                (
                                    new[]
                                    {
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData("На сьогодні", $"methodstart"),
                                                InlineKeyboardButton.WithCallbackData("За датою", $"methodstart"),


                                            }
                                    }
                                );
                await botClient.SendTextMessageAsync(message.Chat.Id, "Дізнатися курс:", replyMarkup: KeyboardMarkup);
                return;

                //await botClient.SendTextMessageAsync(message.Chat.Id, "Курс валют на сьогодні: ");
            }
        }
    }
}





//using System;
//using Telegram.Bot;
//using Telegram.Bot.Extensions;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;
//using Telegram.Bot.Types.ReplyMarkups;
//using Telegram.Bot.Extensions.Polling;
//using Telegram.Bot.Exceptions;

//namespace TG_bot
//{
//    public class EasyPrivatBank_bot
//    {
//        TelegramBotClient botClient = new TelegramBotClient("5427717344:AAESHYHqG70UV9t11M5jZNut5gSmjmTZqow");
//        CancellationToken cancellationToken = new CancellationToken();
//        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

//        public async Task Start()
//        {
//            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
//            var botMe = await botClient.GetMeAsync();
//            Console.WriteLine($"Бот {botMe.Username} почав працювати!");
//            Console.ReadKey();

//        }

//        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
//        {
//            var ErrorMessage = exception switch
//            {
//                ApiRequestException apiRequestException => $"Помилка в телеграм бот АПІ: \n{apiRequestException.ErrorCode}" +
//                $"\n{apiRequestException.Message}",
//                _ => exception.ToString()
//            };
//            Console.WriteLine(ErrorMessage);
//            return Task.CompletedTask;
//        }

//        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
//        {
//            if (update.Type == UpdateType.Message && update?.Message.Text != null)
//            {
//                await HandlerMessageAsync(botClient, update.Message);
//            }
//        }

//        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
//        {
//            if (message.Text == "/start") 
//            {
//                InlineKeyboardMarkup KeyboardMarkup = new
//                (
//                    new[]
//                    {
//                            new[]
//                            {
//                                InlineKeyboardButton.WithCallbackData("Курс валют", $"methodstart"),
//                                InlineKeyboardButton.WithCallbackData("Знайти відділення", $"methodstart"),


//                            },
//                            new []
//                            {
//                                InlineKeyboardButton.WithCallbackData("Знайти термінал", $"methodstart"),
//                                InlineKeyboardButton.WithCallbackData("Власний архів", $"methodstart")
//                            }
//                    }
//                );
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть дію:", replyMarkup: KeyboardMarkup);
//                return;




//                //ReplyKeyboardMarkup replyKeyboard = new
//                //    (
//                //        new[]
//                //        {
//                //            new KeyboardButton[] { "Курс валют", "Власний архів" },
//                //            new KeyboardButton[] { "Знайти відділення", "Знайти термінал" },
//                //        }

//                //    )
//                //{
//                //    ResizeKeyboard = true
//                //};
//                //await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть пункт меню:", replyMarkup: replyKeyboard);
//                //return;





//                //await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть команду \n/exchangerates\n/currency_archive" +
//                //    "\n/pb_offices\n/terminals");
//                //return;
//            }
//            if (message.Text == "Курс валют") 
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "привет");
//            }
//        }
//    }
//}