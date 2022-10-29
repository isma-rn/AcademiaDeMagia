# AcademiaDeMagia
IA – Examen Backend .Net (C#) Developer

Para la ejecución del proyecto, se ocuparon y/o requieren la utilización de las siguientes tecnologías:
1.- Visual Studio 2022
2.- .NET Core 6
3.- SQL Server 2019

Se recomienda el seguimiento de los pasos a continuación:
1: Abrir Visual Studio 2022, En la ventana de inicio se deberá ubicar la sección "Tareas iniciales". Seleccionar la opción "Clonar un repositorio"

2: En la ventana de "Clonar un repositorio" ubicamos la caja de texto "Ubicación del repositorio" e ingresamos la siguiente URL
  https://github.com/isma-rn/AcademiaDeMagia.git
  y seleccionamos la opción "Clonar"

3: Esperamos a que se descarguen los archivos del proyecto, y después debemos ubicar el panel "Explorador de soluciones"
  nota. Si no se ubica el panel podemos dirigirnos al menú: Ver, opción "Explorador de soluciones" 

4: A continuación Ubicamos el proyecto "Academia.API" y damos doble click en appsetting.json para abrirlo

5: En la línea 10 debemos establecer la información de nuestro servidor de base de datos SQL Server, para ello sólo debemos:
  - reemplazar "localhost" por la direccion IP de nuestro Servidor de Base de datos
  - reemplazar "jisma" por el nombre de usuario con que accedamos a SQL Server (éste deberá tener permisos para crear Bases de datos)
  - reemplazar "12345678" por la contraseña del usuario con que accedamos a SQL Server
  - reemplazar AcademiaBD, sólo en caso de que ya contemos con un nombre similar de base de datos en el SQL Server

6: En la parte superior de Visual Studio 2022, ubicamos y damos click en el botón con icono de una triangulo verde, seguido de la leyenda "Academia.API"

7: A continuación, Visual Studio ejecutará la solución y se mostrara el framework Swagger a través del navegador Web
 
