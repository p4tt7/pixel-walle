![PIXEL WALL-E](https://github.com/user-attachments/assets/a4f84b33-bafd-4905-8309-a6ce487c32c2)

Pixel Wall-E es una aplicación interactiva diseñada para crear arte pixelado mediante los comandos brindados por su lenguaje de programación. Invita a explorar la libertad creativa a través de un robot, el cual al controlarlo es capaz de dibujar sobre un canvas cuadriculado. La aplicación cuenta con una interfaz intuitiva y visualmente clara, donde los usuarios pueden escribir su código, ejecutarlo paso a paso y observar cómo un pequeño robot ejecuta las órdenes.



## INSTALACIÓN


### REQUISITOS PREVIOS
- [.NET 6 SDK o superior](https://dotnet.microsoft.com/en-us/download) 
- [Visual Studio 2022 o superior](https://visualstudio.microsoft.com/)

1. Clonar o descargar el repositorio

    - ```git clone https://github.com/p4tt7/sky-maze.git```

2. Abrir el proyecto en Visual Studio y compilar (build) la solución.

3. Luego, ir a la carpeta bin\Debug\net8.0-windows y ejecutar el archivo .exe para iniciar la aplicación.




## INTERFAZ GRÁFICA

<img width="500" alt="ui" src="https://github.com/user-attachments/assets/0e09138b-7856-40f1-890a-c0d59cd5ac1c" />  

_<sub>Vista general de la interfaz visual</sub>_


El programa está compuesto por **tres ventanas principales**:

### Ventana de inicio
Contiene las opciones principales: salir del programa, crear un nuevo proyecto o cargar uno previamente guardado.  
Al crear un nuevo proyecto, se accede a la siguiente ventana.

### Ventana de dimensiones
Permite seleccionar las dimensiones iniciales del canvas.

### Ventana principal
Aquí es donde sucede la magia: encontramos el editor de texto, el canvas y una serie de botones que facilitan la interacción del usuario con el programa.


A continuación, una descripción detallada de cada sección:

---

#### Editor de texto  
Área donde el usuario escribe el código que se va a ejecutar.

---

#### Canvas  
Se inicializa automáticamente al introducir las dimensiones. En caso de que estas sean rectangulares, el canvas se centra dentro del área disponible.  
Está definido mediante un **`UniformGrid`**, lo que garantiza que cada píxel tenga forma cuadrada y proporciones iguales.  
El canvas se renderiza **solo si el código es sintáctica y semánticamente correcto**.

**Colores disponibles**:
- Rojo
- Azul
- Verde
- Amarillo
- Naranja
- Púrpura
- Negro
- Blanco
- Transparente (no afecta el canvas)

---

#### Botones  
Distribuidos por toda la ventana, permiten acceder rápidamente a funcionalidades clave:

- **Run!**: Es el botón principal. Llama al intérprete y comienza el análisis del código fuente.  
- **Clear**: Borra completamente el contenido del editor de texto.  
- **Errors**: Tras analizar el código, si se detectan errores sintácticos, semánticos o de ejecución, estos se guardan aquí. Al presionarlo, se despliega una tabla que clasifica los errores según su tipo, mensaje explicativo y ubicación (línea, archivo y columna).  
- **Redimension**: Permite cambiar las dimensiones del canvas. Al hacerlo, se elimina el contenido previamente dibujado.  
- **Save**: Guarda el proyecto. Tanto las dimensiones del canvas como el código fuente se almacenan en la ubicación del disco local que el usuario elija.



## TECNOLOGÍAS UTILIZADAS

WPF (Windows Presentation Foundation): Para desarrollar una interfaz gráfica intuitiva y responsive. Dentro de la misma:
- Editor de texto para escribir código, el cual permanece incluso si se cometieron errores.
- Canvas cuadriculado que muestra los píxeles dibujados en tiempo real. Las dimensiones del canvas pueden ser ajustadas por el usuario. El tamaño de las cuadrículas se ajusta en dependencia del tamaño del mismo.
- Botones para ejecutar, cargar, guardar, mostrar los errores, y redimensionar el canvas.


JSON: Para la serialización y almacenamiento de archivos de código, facilitando la portabilidad y el intercambio de proyectos. Este brinda:
- Capacidad para guardar y cargar programas en archivos con extensión .pw.
- Exportación e importación de proyectos para compartir o continuar trabajos posteriores.



## FLUJO DEL PROGRAMA

![flow](https://github.com/user-attachments/assets/f9892b2e-8e56-42e9-af2f-d899822ee619)

Primero, en la inicialización, se crea un Manager que recibe el nombre del archivo, el código fuente y las dimensiones del canvas, y a su vez inicializa un contexto con un ámbito de variables y configuración del canvas, un analizador léxico (LexicalAnalizer) y una lista de errores. Luego, durante la compilación, el código fuente pasa por un análisis léxico que lo divide en tokens, seguido de un análisis sintáctico que convierte esos tokens en un Árbol de Sintaxis Abstracta (AST) mediante un Parser, y finalmente un análisis semántico que verifica la validez lógica del programa, como variables declaradas y tipos correctos. Si se detectan errores en cualquiera de estas fases, se muestran al usuario y el proceso se detiene. En la fase de ejecución, si la compilación fue exitosa, el programa PixelWalle se ejecuta instrucción por instrucción, donde cada instrucción puede modificar el estado del contexto, incluyendo variables y la posición en el canvas, mientras que el renderizador CanvasRender actualiza la interfaz gráfica tras cada instrucción. Si ocurren errores en tiempo de ejecución, como divisiones por cero o etiquetas no definidas, estos se muestran y la ejecución se detiene. Finalmente, si no hay errores, se muestra un mensaje de éxito y el resultado final se refleja en el canvas.




## ARQUITECTURA

Pixel Wall-E está diseñado siguiendo una arquitectura modular que separa claramente las responsabilidades entre interfaz gráfica, lógica de negocio y controladores. Esta organización sigue principios similares a los del patrón MVC (Modelo-Vista-Controlador), asegurando que cada componente del sistema esté enfocado en una única responsabilidad. 
A continuación, se presenta un desglose de las principales carpetas del proyecto y su función dentro de la arquitectura general:



| Carpeta/Archivo                | Contenido y responsabilidad                                                                                                                                                                                                                                                                                                                                                                                                                  |
| ------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **controllers/**               | Conexión entre el backend y el frontend. Contiene clases controladoras que vinculan la lógica interna con la visualización sin romper el principio de separación de responsabilidades. Entre dichas clases se encuentran el intérprete de comandos, el renderizador del canvas, el lector del código fuente y el manejador de errores, que capta los errores generados por el intérprete y los muestra en la interfaz a través de una tabla. |
| **src/**                       | Núcleo lógico del proyecto. Contiene los modelos de datos y la lógica fundamental. Se divide en varias subcarpetas: el *Lexer*, que define los tokens y el tokenizador; el *Parser*; el *AST* (Árbol de Sintaxis Abstracta), donde se definen las operaciones, tipos y funciones permitidas; y el *Context* del programa, que representa el entorno de ejecución.                                                                            |
| **views/**                     | Componentes visuales personalizados que complementan las ventanas principales. Aquí se ubican vistas auxiliares y controles reutilizables que enriquecen la interfaz gráfica.                                                                                                                                                                                                                                                                |
| **assets/**                    | Recursos gráficos estáticos como íconos, imágenes o fondos. Estos elementos se utilizan dentro de la interfaz para mejorar la experiencia visual.                                                                                                                                                                                                                                                                                            |
| **\*.xaml**                    | Vistas gráficas principales del proyecto escritas en XAML. Incluyen `App.xaml`, `MainWindow.xaml`, `StartingWindow.xaml`, `DimensionMenu.xaml` y `ErrorsWindow.xaml`. Cada una tiene su correspondiente archivo `.xaml.cs` asociado, que contiene la lógica de eventos y la inicialización de componentes.                                                                                                                                   |
| **pixel\_walle.sln / .csproj** | Archivos de configuración del proyecto para Visual Studio. Especifican los archivos del proyecto, sus dependencias y las opciones de compilación. Gestionan cómo se construye, ejecuta y mantiene la aplicación.                                                                                                                                                                                                                             |



