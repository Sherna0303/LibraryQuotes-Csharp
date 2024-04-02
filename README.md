
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

4. Una vez abierto puedes iniciar el proyecto con el boton RUN con el texto "https".

## Usage/Examples

1. Para usar los edpoints debemos hacer uso de la siguiente ruta:
```
http://localhost:5203
```
2. Ahora podemos hacer uso del primer endPoint.
```
http://localhost:5203/calculateCopyPrice
{
  "Name": "Libro",
  "Author": "Libro",
  "Price": 22,
  "Type": 1
}
```
3. Ahora podemos hacer uso del segundo endPoint,
```
http://localhost:5203/calculateListCopyPrice
[
    {
        "Name": "Libro",
        "Author": "Libro",
        "Price": 20,
        "Type": 0
    },
    {
        "Name": "Novela",
        "Author": "Novela",
        "Price": 20,
        "Type": 1
    }
]
```
