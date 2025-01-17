# DelegateAndEventsLab

Этот проект демонстрирует использование делегатов и событий в C# для реализации поиска файлов, а также нахождения минимальной и максимальной длины пути среди найденных файлов.

## Описание

Программа принимает на вход параметры командной строки, указывающие директорию для поиска и шаблон имени файла. Она использует класс `FileSearcher` для рекурсивного обхода директорий и генерации событий при нахождении каждого файла. Обработчики событий используются для сбора информации о найденных файлах и управления процессом поиска. После завершения поиска программа выводит список найденных файлов, а также минимальную и максимальную длину пути среди них.

## Функциональность

*   Поиск файлов по заданному шаблону в указанной директории и её поддиректориях.
*   Использование событий `FileFoundEvent` и `PostProcessingEvent` для обработки найденных файлов и управления процессом поиска.
*   Возможность отмены поиска после нахождения первого файла (по умолчанию) или продолжения поиска всех файлов (с помощью параметра `--all true`).
*   Нахождение минимальной и максимальной длины пути среди найденных файлов с использованием методов расширения `GetMin` и `GetMax`.
*   Обработка исключений `UnauthorizedAccessException` и `PathTooLongException` во время обхода директорий.
*   Парсинг аргументов командной строки с помощью словаря.

## Использование

Для запуска программы необходимо использовать командную строку в следующем формате:

DelegateAndEventsLab.exe --directory <путь_к_директории> --searchpattern <шаблон_поиска> [--all <true/false>]

*   `--directory`: Обязательный параметр. Путь к директории, в которой будет производиться поиск.
*   `--searchpattern`: Обязательный параметр. Шаблон поиска файлов (например, `*.txt`, `MyFile*.cs`).
*   `--all`: Необязательный параметр. Если установлено в `true`, будут найдены все файлы, соответствующие шаблону. Если `false` (по умолчанию), поиск остановится после первого найденного файла.

**Пример:**

DelegateAndEventsLab.exe --directory E:\MyProjects --searchpattern *.cs --all true

## Вывод

Программа выводит в консоль следующие сообщения:

*   Сообщение о каждом найденном файле в формате "Найден файл: <имя_файла> в папке: <полный_путь>".
*   Минимальная длина пути: "Min path: <минимальный_путь>".
*   Максимальная длина пути: "Max path: <максимальный_путь>".

Если файлы не найдены, программа выводит сообщение "Files not found".

## Структура проекта

*   `DelegateAndEventsLab`: Основной проект, содержащий класс `Program`.
*   `FileWalker`: Проект, содержащий класс `FileSearcher` и модели событий (`FileArgs`, `FileSearcherArgs`).
*   `MaxFinderWithEvents`: Проект, содержащий методы расширения `GetMin` и `GetMax`.

## Зависимости

*   .NET Runtime (версия, указанная в файле проекта)

## Классы и события

*   `FileSearcher`: Класс, выполняющий поиск файлов и генерирующий события.
*   `FileFoundEvent`: Событие, генерируемое при нахождении файла.
*   `PostProcessingEvent`: Событие, генерируемое после обработки файла или директории.
*   `FileArgs`: Класс аргументов события `FileFoundEvent`. Содержит информацию о файле (`FullPath`, `FileName`, `Path`, `Cancel`).
*   `FileSearcherArgs`: Класс аргументов события `PostProcessingEvent`. Наследуется от `FileArgs` и содержит дополнительную информацию о процессе поиска (`CurrentDirectory`, `OldDirectory`, `Pattern`).
*   `EnumerableExtensions`: Класс, содержащий методы расширения `GetMin` и `GetMax` для `IEnumerable<T>`.

## Среда разработки

*   **IDE:** Visual Studio Code
*   **Версия .NET:** .NET 8

## Использование (запуск)

1.  Клонируйте репозиторий.
2.  Откройте проект в Visual Studio Code.
3.  Убедитесь, что у вас установлен .NET 8 SDK. Вы можете скачать его с [официального сайта Microsoft](https://dotnet.microsoft.com/download).
4.  Откройте терминал в Visual Studio Code и перейдите в директорию проекта, содержащую файл `.csproj`.
5.  Выполните следующие команды:

    ```sh
    dotnet build
    dotnet run -- --directory <путь_к_директории> --searchpattern <шаблон_поиска> [--all <true/false>]
    ```
    Замените `<путь_к_директории>` и `<шаблон_поиска>` на нужные значения. Например:
    ```sh
    dotnet run -- --directory E:\MyProjects --searchpattern *.cs --all true
    ```
6.  Проверьте вывод консоли.