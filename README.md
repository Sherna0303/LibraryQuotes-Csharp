
# LibraryQuotes-Csharp
Proyecto Realizado en el lenguaje de programación C# y con el FrameWork .NET


## Installation
1. Clona el repositorio desde GitHub:

```
git clone https://github.com/Sherna0303/LibraryQuotes-Csharp.git
```


2. Accede al repositorio desde consola bash de git:
```
cd LibraryQuotes
```

3. Ahora puedes abrir el proyecto utilizando de preferencia el entorno de desarrollo VisualStudio.

4. Instala en NuGet la libreria "FlientValidation" para las validaciones de entrada.

5. Una vez abierto puedes iniciar el proyecto con el boton RUN con el texto "https".

## Usage/Examples

1. Para usar los edpoints debemos hacer uso de la siguiente ruta:
```
http://localhost:5203
```
2. Ahora podemos hacer uso del primer endPoint.
```
http://localhost:5203/calculateCopyPrice
{
    "AntiquityYears":0,
    "Copies": [
        {
            "Name": "Novela 1",
            "Author": "Autor 2",
            "Price": 15,
            "Type": 1
        }
    ]
}
```
3. Ahora podemos hacer uso del segundo endPoint,
```
http://localhost:5203/calculateListCopyPrice
{
    "AntiquityYears":0,
    "Copies": [
        {
            "Name": "Libro 1",
            "Author": "Autor 1",
            "Price": 10,
            "Type": 0
        },
        {
            "Name": "Novela 1",
            "Author": "Autor 2",
            "Price": 15,
            "Type": 1
        }
    ]
}
```
4. Ahora podemos hacer uso del tercer endPoint,
```
http://localhost:5203/calculateBudget
{
    "Budget":300,
    "ClientCopies": {
    "AntiquityYears":0,
    "Copies": [
            {
                "Name": "Libro 1",
                "Author": "Autor 1",
                "Price": 10,
                "Type": 0
            },
            {
                "Name": "Novela 1",
                "Author": "Autor 2",
                "Price": 15,
                "Type": 1
            }
    ]
}
```
