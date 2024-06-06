# ToyStore - Магазин игрушек

Тема:

Продажа игрушек

Описание предметной области:

Онлайн-магазин "ЯНеПридумалЕщёНазвание" занимается продажей игрушек разных категорий и направленостей. У игрушек есть категории, страны производители, производяшии их компании, поставщики, розничные и оптовые цены, описание и возрастные категории.

# Таблицы пользователей:

-Пользователи (айди пользователя, логин, пароль, айди роли)

-Роли пользователей (айди роли, роль)

# Таблицы товара:

-Игрушки (айди игрушки, название, айди категории, айди страны, айди производителя, айди поставщика, цена оптовая, цена розничная, айди возрастной категории, описание, изображение)

-Категории (айди категории, категория)

-Страны производители (айди страны, страна)

-Производители (айди производителя, производитель)

-Поставщики (айди поставщика, поставщик)

-Возрастные категории (айди возрастной категории, возрастная категория)

# Таблицы заказов:

-Заказы (айди заказа, номер заказа, айди игрушки, количество товара)

-Справочник (айди справочника, айди пользователя, номер заказа)

# Приложение содержит:

-Окно регистрации ()

-Окно авторизации ()

-Окно личного кабинета пользователя ()

-Окто каталога товаров ()

-Окно добавления/редактирования товаров ()

-Окно корзины ()

-Окно оформления заказа ()

# Скрины:

Диаграмма структуры БД (14.05.24):

![Диаграмма БД](https://github.com/AndryDewsden/ToyStoreByVlasovAndry/assets/154083401/ca57acb7-141e-4ee3-beca-c561125b20ef)

Таблица Игрушки (14.05.24):

![Данные таблицы](https://github.com/AndryDewsden/ToyStoreByVlasovAndry/assets/154083401/0dc838ff-b32b-40d4-a308-6cdce173acca)

Окно авторизации:



Вывод данных:



# Чек-лист:

База данных:

- Структура базы -выполненно

- Заполнение данными -выполненно

Приложение:

- Регистрация/авторизация пользователя -выполненно

- Личный кабинет пользователя в двух вариациях -выполненно

- Список товаров -выполненно

- Добавление товаров -выполненно

- Редактирование товаров -выполненно

- Удаление товаров -выполненно

- Добавление товаров в корзину -выполненно

- Оформление заказа -выполненно

- Дизайн -выполненно

- Генерация штрихкода -выполненно

- Генерация pdf документа -выполненно

- Отображение картинок -выполненно

- Загрузка изображение -выполнено, но не работает

- Редактирование пользователей -выполненно

- Редактирование заказов -выполненно
