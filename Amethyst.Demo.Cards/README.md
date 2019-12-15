# Ets.Services.Cards

Микросервис платежных карт из демонстрационного пакета event sourcing платформы Amethyst.

## Описание

Демонстрационный микросервис для управления платежными картами. Получает команды по протоколу gRPC от Gateway.
При изменении состояния своего агрегата генерирует события и добавляет их в InMemory-коллекцию для последующего 
атомарного сохранения в хранилище Amethyst и публикации на шину обмена данными.

## Методологии

* DDD
* Event Sourcing

## Технологический стек

* C# 7.3
* .NET Core 2.2
* gRPC
* \(Optional) Apache Kafka
* \(Optional) PostgreSQL

## Слои сервиса

0. **Host.** Хост микросервиса, точка входа, подключение зависимостей.
0. **Scheduler** Сервис деактивации истекших платежных карт по расписанию.
0. **Grpc.** Объявление gRPC-контрактов микросервиса, клиент для его вызова.
0. **Application.** Сервисы инкапсулирующие бизнес-логику вызова агрегата и работу с репозиториями.
0. **Domain.** Доменная модель микросервиса и контракты агрегатов.
0. **Events.** События генерируемые микросервисом.
0. **Events.Protobuf.** Библиотека для protobuf сериализации событий микросервиса

## Генерируемые события 

#### Card (aggregate root)

- BalanceDecreased
- BalanceIncreased
- CardActivated
- CardBlocked
- CardCreated
- CardRemoved
- CardExpired
- CardRenamed

#### Scheduled 

- EffectivePeriodEnded

## Примитивы

#### CardState

- [ ] None = 0
- [x] Active = 1
- [ ] Blocked = 2
- [ ] Expired = 3

## gRPC вызовы сервиса

#### CardGrpc

Инжект клиента и каналов нотификации с помощью DI контейнера

    private readonly CardGrpc.CardGrpcClient _client;
    private readonly INotificationsChannel _notificationsChannel;
    private readonly INotificationContext _notificationContext;
    ...
    public CardService(
       CardGrpc.CardGrpcClient client,
       INotificationsChannel notificationsChannel, 
       INotificationContext notificationContext)
    {
       _client = client;
       _notificationsChannel = notificationsChannel;
       _notificationContext = notificationContext;
    }
    
Создание карты

    using (var notification = await _notificationsChannel.StartListenAsync(_notificationContext.CurrentId))
    {
        await _client.CreateAsync(request);
        await notification.ThrowIfExpired();
        return cardId;
    }
    
Удаление карты

    using (var notification = await _notificationsChannel.StartListenAsync(_notificationContext.CurrentId))
    {
        await _client.RemoveAsync(request);
        await notification.ThrowIfExpired();
        return cardId;
    }
    
Смена имени
 
    using (var notification = await _notificationsChannel.StartListenAsync(_notificationContext.CurrentId))
    {
        await _client.RenameAsync(request);
        await notification.ThrowIfExpired();
        return cardId;
    }
    
Активация карты
 
    using (var notification = await _notificationsChannel.StartListenAsync(_notificationContext.CurrentId))
    {
        await _client.ActivateAsync(request);
        await notification.ThrowIfExpired();
        return cardId;
    }
    
Блокировка карты
 
    using (var notification = await _notificationsChannel.StartListenAsync(_notificationContext.CurrentId))
    {
        await _client.BlockAsync(request);
        await notification.ThrowIfExpired();
        return cardId;
    }
    
Пополнение карты

    using (var notification = await _notificationsChannel.StartListenAsync(_notificationContext.CurrentId))
    {
        var result = await _client.IncreaseBalanceAsync(request);
        if (!result.Version.HasValue)
            return;
         await notification.Wait(TimeSpan.FromSeconds(60), result.Version);
    }
    
Списание с карты

    using (var notification = await _notificationsChannel.StartListenAsync(_notificationContext.CurrentId))
    {
        var result = await _client.DecreaseBalanceAsync(request);
        if (!result.Version.HasValue)
            return;
         await notification.Wait(TimeSpan.FromSeconds(60), result.Version);
    }

## Amethyst Pro
    
License [GNU GPLv3](http://www.gnu.org/licenses/gpl-3.0.txt)

© [Amethyst Pro](http://wwww.amethyst.pro) LLC, 2019