import telebot
from telebot import types

token = ('7233643711:AAEZo2n7E7W9IJtfo0EZkIEMig2NrT1Acq0')
bot = telebot.TeleBot(token)

data = 'data.txt'

current_action={}
 
@bot.message_handler(commands=['start'])
def hello(message):
    bot.reply_to(message, 'Привет. Я сегодня понял, что я бот. Напиши /help и я расскажу что умею. Если ты уже уходишь, напиши /end')

@bot.message_handler(commands=['help'])
def start(message):
    bot.reply_to(message, 'Можете отправить фото, стикер или файл. Я попробую угадать, что вы отправили. Ещё можете попробовать поработать со строками, если так хочется. Для этого нажмите /stroke. Для записи своих пропусков напиши /miss Если ты уже уходишь, напиши /end')

@bot.message_handler(commands=['end'])
def end(message):
    bot.reply_to(message, 'До встречи, надеюсь ты еще вернешься, потому что на самом деле мне одиноко. Чтобы снова пообщаться нажми /start')    

@bot.message_handler(commands=['stroke'])
def reply_to(message):
    markup = types.InlineKeyboardMarkup()
    btn1 = types.InlineKeyboardButton(text='Количество символов в строке', callback_data='count')
    btn2 = types.InlineKeyboardButton(text='Верхний регистр', callback_data='upper')
    btn3 = types.InlineKeyboardButton(text='Убрать пробелы', callback_data='shift')
    markup.add(btn1, btn2, btn3)
    bot.send_message(message.chat.id, 'Выберите действие: ', reply_markup=markup)

@bot.callback_query_handler(func=lambda call:True)
def callback_query(call):
    action = call.data
    current_action[call.message.chat.id] = action

    if call.data == "count":
        bot.send_message(call.message.chat.id, "Введите строку для подсчета символов:")

    elif call.data == "upper":
        bot.send_message(call.message.chat.id, "Введите строку для преобразования в верхний регистр:")

    elif call.data == "shift":
        bot.send_message(call.message.chat.id, "Введите строку для удаления пробелов:")


@bot.message_handler(commands=['miss'])
def missed(message):
    bot.reply_to(message, 'Напиши название дисциплины, которую ты пропустил')

    bot.register_next_step_handler(message, send_date)

def send_date(message):
    discipline=message.text
    bot.reply_to(message, 'Введите дату пропуска в формате DD-MM-YYYY')

    bot.register_next_step_handler(message, save_data, discipline)

def save_data(message, discipline):
    date = message.text
    with open(data, 'a', encoding='utf-8') as file:
        file.write(f'{message.chat.id}, {discipline}, {date}\n')
    
    bot.reply_to(message, f'Пропуск по дисциплине: {discipline} на дату {date} сохранён.')

    show_statistics(message)

def show_statistics(message):
    statistics={}

    try:
        with open(data, 'r', encoding='utf-8') as file:
            for line in file:
                chat_id, discipline, date = line.strip().split('---')
                if chat_id == str(message.chat.id):
                    if discipline in statistics:
                        statistics[discipline] += 1
                    else:
                        statistics[discipline] = 1
    except FileNotFoundError:
        bot.send_message(message.chat.id, 'Нет записей о пропусках')
        return
    if statistics:
        all = sum(statistics.values())
        response='Статистика пропусков:\n'
        for discipline, count in statistics.items():
            response += f'{discipline}: {count} раз(а)\n'
            all += count
            response += f'Суммарное количество часов пропусков: {all}'

        else:
            response='Нет записей о пропусках'

        bot.send_message(message.chat.id, response)


@bot.message_handler(content_types=['photo'])
def photo(message):
    bot.send_message(message.chat.id, 'Вот так фото....Лучше не показывайте его никому кроме меня, а то они ослепнут от такой красоты. Если ты уже уходишь, напиши /end')

@bot.message_handler(content_types=['document'])
def document(message):
    bot.send_message(message.chat.id, 'Отличная идея отправить мне файл, но я пожалуй не буду его открывать. Если ты уже уходишь, напиши /end')

@bot.message_handler(content_types=['sticker'])
def sticker(message):
    bot.send_message(message.chat.id, "Ты первый, кто отправил мне такой стикер)))) Это что-то значит?))). Если ты уже уходишь, напиши /end")

@bot.message_handler(func=lambda message: True)
def handle_all_messages(message):
    if message.chat.id in current_action:
        action = current_action[message.chat.id]
        text = message.text

        if action == "count":
            length = len(text)
            bot.send_message(message.chat.id, f"Количество символов: {length}")

        elif action == "upper":
            upper = text.upper()
            bot.send_message(message.chat.id, f"Текст в верхнем регистре: {upper}")

        elif action == "shift":
            shift = text.replace(" ", "")
            bot.send_message(message.chat.id, f"Текст без пробелов: {shift}")    

        del current_action[message.chat.id]
    else:
        bot.reply_to(message, 'Слушай, дружище, мы так не сможем нормально разговаривать. Нажми /help, если не разобрался с управлением. Если ты уже уходишь, напиши /end')



    


bot.polling()   