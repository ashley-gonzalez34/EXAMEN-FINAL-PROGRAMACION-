using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace EXAMEN_FINAL_PROGRAMACION___
{
    class Program
    {


        //Key del bot
        private static readonly TelegramBotClient Bot = new TelegramBotClient("API token aqui");
        static void Main(string[] args)
        {
            

            //Método que se ejecuta cuando se recibe un mensaje
            Bot.OnMessage += BotOnMessageReceived;

            //Método que se ejecuta cuando se recibe un callbackQuery
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            //Método que se ejecuta cuando se recibe un error
            Bot.OnReceiveError += BotOnReceiveError;

            //Inicia el bot
            Bot.StartReceiving();
            Console.WriteLine("Bot levantado!");//avisa que el boot esta levantado en la consola 
            Console.ReadLine();
            Bot.StopReceiving();

            Console.WriteLine("Bot Apagado!");//avisa que el boot esta apagado en la consola
            Console.ReadLine();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.Text) return;

            switch (message.Text.Split(' ').First())
            {
                //Enviar un inline keyboard con callback
                case "/1Calzado":

                    //Simula que el bot está escribiendo y el cliente asi esta pendiente del que el boot esta contestando
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(50);

                    var keyboard1 = new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {    
                        InlineKeyboardButton.WithCallbackData(//<-nos permite crear un botón, con un texto
                            text:"zapatillas ",// mostrara la imagen 
                            callbackData: "zapatillas"),

                         InlineKeyboardButton.WithCallbackData(
                            text:"Tennis ",
                            callbackData: "Tennis"),
                        
                        InlineKeyboardButton.WithCallbackData(
                            text:"Tacones ",
                            callbackData: "Tacones"),

                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Ubicación con info",//boton de la ubicacion 
                            callbackData: "GPS"),
                        InlineKeyboardButton.WithCallbackData(
                            text:"Contacto",
                            callbackData: "contacto"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Precio zapatillas",
                            callbackData: "Precio zapatillas"),//Precio del calzado 

                        
                    },

                     new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Precio Tennis",
                            callbackData: "Precio Tennis"),//Precio 

                        InlineKeyboardButton.WithCallbackData(
                            text:"Precio Tacones",
                            callbackData: "Precio Tacones"),//Precio 

                        
                    }

                });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Elija una opción",
                        replyMarkup: keyboard1);
                    break;

                case "/2Ropa":

                    var keyboard2 = new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text: "Blusas",
                            callbackData: "Blusas"),
                         InlineKeyboardButton.WithCallbackData(
                            text:"Vestidos ",
                            callbackData: "Vestidos"),
                         
                        InlineKeyboardButton.WithCallbackData(
                            text:"Faldas ",
                            callbackData: "Faldas"),

                        

                    },
                    new []
                    {
                       
                        InlineKeyboardButton.WithCallbackData(
                            text:"Contacto.",
                            callbackData: "Contacto."),

                        InlineKeyboardButton.WithCallbackData(
                            text:"Precio Vestido",
                            callbackData: "Precio Vestido"),//Precio 

                        



                    },
                    new []
                    {

                        
                        InlineKeyboardButton.WithCallbackData(
                            text:"Precio Blusas",
                            callbackData: "Precio Blusas"),//Precio del calzado precio
                        InlineKeyboardButton.WithCallbackData(

                            text:"Precio Faldas",
                            callbackData: "Precio Faldas"),




                    },
                    
                });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Elija una opción",
                        replyMarkup: keyboard2);
                    break;


                case "/3":

                    var keyboard3 = new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Volver",
                            callbackData: "Volver"),
                        InlineKeyboardButton.WithCallbackData(
                            text:"Reply",
                           callbackData: "reply"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Reenviar",
                            callbackData: "reenviar"),

                    },
                    
                });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Elija una opcion",
                        replyMarkup: keyboard3);
                    break;

                //Mensaje por default
                default:
                    const string usage = @"      
                Eliga una de estas  opciones: 
                /1Calzado 
                /2Ropa
                /3";//aca estan las opciones que quiero que funcionen en el boot 

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        text: usage,
                        replyMarkup: new ReplyKeyboardRemove());

                    break;
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery; // El usuario pulsará el botón  para realizar la acción deseada
                                                                         //enviando al sistema un callback query con la elección.Tras pulsarlo el bot
            switch (callbackQuery.Data)
            {
                case "Volver":
                    ReplyKeyboardMarkup tipoContacto = new[]
                    {
                        new[] {  "Volver al inicio " },// en esta parte lo que hace es volver de nuevo a las opciones
                      
                    };

                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,//boton de volver 
                        text: "presione el boton de volver", //da un aviso 
                        replyMarkup: tipoContacto);
                    break;

              
                case "GPS":
                    await Bot.SendVenueAsync(// para que funcione sincronizadamente la ubicacion 
                        chatId: callbackQuery.Message.Chat.Id,
                        latitude: 14.33365f,//aca represta las coordenaas del lugar donde se ubica el negocio
                        longitude: -89.71094f,
                        title: "Boutique MODA",//el nombre del lugar 
                        address: "ASUNCION MITA; JUTIAPA" // direccion 
                        );
                    break;

                case "zapatillas"://foto de zapatillas 
                    await Bot.SendPhotoAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        photo: "https://www.google.com/imgres?imgurl=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F8d%2Fb1%2Fe0%2F8db1e05291b34aa53a830b38ded8bc64.png&imgrefurl=https%3A%2F%2Fwww.pinterest.com.mx%2Fpin%2F796996465275854520%2F&tbnid=01_1_V-Eaz8TGM&vet=12ahUKEwil1OPq1__wAhXoeDABHQ5SCIcQMygDegUIARDaAQ..i&docid=-kT6yXvb3n-kKM&w=453&h=595&q=ZAPATOS%20CKLASS&ved=2ahUKEwil1OPq1__wAhXoeDABHQ5SCIcQMygDegUIARDaAQ"
                        );
                    break;

                case "Tennis"://foto de tennis
                    await Bot.SendPhotoAsync(//esta opcion permite colocar un foto colocando el link 
                        chatId: callbackQuery.Message.Chat.Id,
                        photo: "https://zapachic.com/wp-content/uploads/2018/07/Tenis-Blancos-Cklass.jpg"// la imagen debe de ser pgn para que funciones
                        );
                    break;

                case "Tacones"://foto de tacones 
                    await Bot.SendPhotoAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        photo: "https://zapachic.com/wp-content/uploads/2018/09/2-3.jpg"
                        );
                    break;


                case "Vestidos":
                    await Bot.SendPhotoAsync(
                        chatId: callbackQuery.Message.Chat.Id,//foto de vestidos
                        photo: "https://www.google.com/imgres?imgurl=https%3A%2F%2Fi.pinimg.com%2F474x%2F0b%2F35%2F7b%2F0b357b8d0a1090935a2d0cc399fd8daf.jpg&imgrefurl=https%3A%2F%2Fwww.pinterest.com.mx%2Fpin%2F687924911813412506%2F&tbnid=ICtGjY4KVoO_LM&vet=12ahUKEwjTkYP56oDxAhUoYzABHTNvAg4QMygDegUIARDUAQ..i&docid=gqFXi4ff79G8JM&w=454&h=580&q=vestidos%20DE%20CLASS&ved=2ahUKEwjTkYP56oDxAhUoYzABHTNvAg4QMygDegUIARDUAQ"
                        );
                    break;

                case "Blusas":
                    await Bot.SendPhotoAsync(
                        chatId: callbackQuery.Message.Chat.Id,//Foto de blusas
                        photo: "https://ahorra-ya.mx/public/gimg/7/1/6/6/5/3/716653-900-100000.jpg"
                        );
                    break;
                case "Faldas":
                    await Bot.SendPhotoAsync(
                        chatId: callbackQuery.Message.Chat.Id,//Foto de blusas
                        photo: "https://http2.mlstatic.com/D_NQ_NP_989518-MLM42994585515_082020-W.jpg"
                        );
                    break;
                    


                case "Precio zapatillas"://yo quise agregar una animacion para que no sea vea aburrida la aplicacion
                    await Bot.SendAnimationAsync(// y con esto yo puse la opcion del precio del producto 
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://media.giphy.com/media/07Fb1i5sLub8wG5X1M/giphy.gif"
                        );
                    break;

                case "Precio Tennis":
                    await Bot.SendAnimationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://media.giphy.com/media/bMlV4V843dfWSipnM6/giphy.gif"
                        );
                    break;

                case "Precio Tacones":
                    await Bot.SendAnimationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://media.giphy.com/media/YMQLL0dtJZ9tVOY7L7/giphy.gif"
                        );
                    break;
                case "Precio Vestido":
                    await Bot.SendAnimationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://media.giphy.com/media/ywx39d9amRtaXLqyLp/giphy.gif"
                        );
                    break;
                case "Precio Faldas"://yo quise agregar una animacion para que no sea vea abrridp la aplicacion
                    await Bot.SendAnimationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://media.giphy.com/media/gC7F0zHt6a7rKAplSz/giphy.gif"
                        );
                    break;
                case "Precio Blusas"://yo quise agregar una animacion para que no sea vea abrridp la aplicacion
                    await Bot.SendAnimationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://media.giphy.com/media/U3YnBEp6MTORGJH9Gh/giphy.gif"
                        );
                    break;

                


                case "reply":
                    await Bot.SendTextMessageAsync(
                       chatId: callbackQuery.Message.Chat.Id,
                        text: "ID: " + callbackQuery.Message.MessageId + " - " + callbackQuery.Message.Text,
                        replyToMessageId: callbackQuery.Message.MessageId);
                    break;

                case "contacto":// este es para que el boton de contacto para que salga el numero de celular 
                    await Bot.SendContactAsync(
                        chatId: callbackQuery.Message.Chat.Id,// dara el contacto de la persona 
                        phoneNumber: "52362595",
                        firstName: "Ashley",
                        lastName: "Gonzalez"
                        );
                    break;

                case "Contacto.":
                    await Bot.SendContactAsync(//sincronizado para que funcione dar el contaacto 
                        chatId: callbackQuery.Message.Chat.Id, 
                        phoneNumber: "25413651",// 
                        firstName: "Camila",
                        lastName: "Gonzalez"
                        );
                    break;

                

                case "reenviar"://reenviara el mesaje para otros contactos
                    await Bot.ForwardMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        fromChatId: callbackQuery.Message.Chat.Id,
                        messageId: callbackQuery.Message.MessageId
                        );
                    break;
            }
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }
    }


} 


