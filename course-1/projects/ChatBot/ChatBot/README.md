# ChatBot - Telegram бот с интеграцией OpenRouter

## Описание проекта

Telegram-бот на ASP.NET Core, который принимает сообщения пользователя через webhook, сохраняет историю диалога, отправляет контекст в OpenRouter и возвращает ответ от LLM.

## Технологии

- ASP.NET Core
- Telegram Bot API
- OpenRouter API
- Serilog (логирование)
- Swagger (документация API)

## Функциональность

### Основные команды

| Команда | Описание |
|---------|----------|
| /start | Приветствие и краткая инструкция |
| /help | Список всех доступных команд |
| /stats | Статистика чата (количество сообщений и токенов) |
| /clear | Очистка истории переписки |
| /summarize | Краткий пересказ диалога |
| /undo | Удаление последней пары сообщений |
| /model | Изменение модели (пример: /model gpt-3.5-turbo) |
| /system | Добавление системного сообщения |

### Обычные сообщения

Любое текстовое сообщение отправляется в OpenRouter с учётом истории диалога. Ответ ассистента сохраняется и возвращается пользователю.

## Установка и запуск

### Требования

- .NET 8 SDK
- Токен Telegram бота (через BotFather)
- API ключ OpenRouter

### Настройка

1. Скопируйте репозиторий

2. Создайте файл `appsettings.json` со следующей структурой:
{
"Telegram": {
"BotToken": "YOUR_TELEGRAM_BOT_TOKEN"
},
"ChatApi": {
"BaseUrl": "https://openrouter.ai/api/v1/chat/completions",
"ApiKey": "YOUR_OPENROUTER_API_KEY",
"DefaultModel": "gpt-3.5-turbo",
"MaxTokens": 1000,
"Temperature": 0.7
}
}

text

3. Запустите приложение:
dotnet run

text

4. Настройте webhook с помощью ngrok или другого туннельного сервиса:
ngrok http 5000

text

5. Установите webhook для вашего бота:
https://api.telegram.org/bot<YOUR_TOKEN>/setWebhook?url=<NGROK_URL>/api/update

text

## Архитектура

### Структура проекта
ChatBot/
├── Commands/ # Реализация команд бота
├── Controllers/ # API контроллеры
├── Repositories/ # Репозитории для хранения данных
├── Dtos/ # DTO модели
├── Settings/ # Настройки приложения
└── Program.cs # Точка входа

text

### Основные компоненты

- `TelegramUpdateProcessor` - обработка входящих обновлений
- `ChatModelRepository` - хранение истории диалогов в памяти
- `HttpChatApiClient` - клиент для взаимодействия с OpenRouter
- `IBotCommand` - интерфейс для всех команд бота

## API Endpoints

| Метод | Путь | Описание |
|-------|------|----------|
| POST | /api/update | Приём webhook обновлений от Telegram |
| POST | /api/chat/test | Тестовый эндпоинт для проверки интеграции с OpenRouter |

## Логирование

Настроено логирование с помощью Serilog:
- Логи выводятся в консоль
- Логи сохраняются в файлы с ежедневной ротацией (папка `logs/`)