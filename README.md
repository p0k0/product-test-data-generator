# product-test-data-generator

***Задание:***

Написать две программы на .NET (core или framework) для решения следующих задач. 

1. Сгенерировать файл (в любом формате) с 10000-ми записей о некоторых товарах. Каждая запись содержит:

   a. номер по порядку,

   b. наименование,

   c. цена  (случайное число от 1 до 1000).

2. Прочитать файл с товарами и «раскидать» файл с товарами в несколько файлов. В каждом файле на выходе должны быть товары с ценами из диапазонов:

   от 1 до 100,

   от 101 до 200,

   ...,

   от 901 до 1000.

***
*(При наличии скомпилированных программ 'wildcard_symbol'.exe)*

Для генерации продуктов выполните.

```
generate products to file:
  generator.exe --name products.dat
```

Для сегрегации сгенерированных продуктов выполните.

```
separate (by price) generated products from file to files:
  separator.exe --in products.dat --out product-0.dat ... product-9.dat
```

*(При запуске из IDE Visual Studio Code)*

Запустите следующие *таски (tasks)* 
Для этого вызовете *Command Pallete* через *UI* или *hot-keys* `ctrl + shift + B`

1. debug generator app(file)

2. debug separator app(file)

3. see result at data folder

***
**p.s. please dont't forget create "data" folder at product-test-data-generator if it doesn't create automatically**