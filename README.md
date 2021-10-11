# ClothesAPI

Задание Написать REST API сервис по взаимодействию с csv файлом(во вложении письма). Файл является аналогом БД. Необходимо реализовать CRUD операции, а именно:
1.1. Получение товара по ID (входной параметр - ID, возвращает - сущность Product с полями как в csv)
1.2. Создание товара (входной параметр - сущность Product, возвращает - только что созданная сущность Product)
1.3. Фильтрация товаров по полю Title (нестрогое равенство) (входной параметр - Title, возвращает - массив Product, подходящий под условие)
1.4. Создать отчет по колличеству всех товаров и суммарной стоимости, сгруппированный по полям Type, Category 1, Category 2

Метод не принимает параметров, а только генерирует отчет.



# Результаты
1. Сделаны все 4 этапа задание
2. Добавил Swagger
3. Добавил Nunit(но тестов мало)
