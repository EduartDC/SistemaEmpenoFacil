# SistemaEmpenoFacil
Sistema para la gestion de una casa de empeños

## Estandar de Codificacion
### 1.	Introducción

Para la realización de este proyecto se utilizará el lenguaje de C#, el cual es un lenguaje de programación moderno, basado en objetos y con seguridad de tipos. Este lenguaje permite a los desarrolladores crear muchos tipos de aplicaciones seguras y sólidas que se ejecutan en .NET
Utilizaremos la plataforma Visual Studio para realizar la codificación, el cuál es un entorno de desarrollo integrado (IDE), un programa con numerosas características que respalda muchos aspectos del desarrollo de software. ¿Por qué lo usaremos?, debido a que el IDE de Visual Studio posee un panel de inicio muy creativo para que se puede usar para editar, depurar y compilar código y, después, publicar la aplicación. Aparte del editor y el depurador estándar que proporcionan la mayoría de IDE que se encuentran en el ambiente de programadores, Visual Studio incluye compiladores, herramientas de finalización de código, diseñadores gráficos y muchas más características para facilitar el proceso de desarrollo de software.

### 2.	Propósito

Este estándar de codificación se puede considerar multi propósito, pues tiene la tarea de servir como referencia para otros programadores, mantener un estilo de codificación, ayudar en el proceso de codificación y mejorar la mantenibilidad del código.
Establecer el conjunto mínimo de requerimientos y recomendaciones técnicas que estandaricen el proceso de desarrollo de software en las fases definidas por las metodologías de aplicación.
### 3.	Normas de nombrado

#### 3.1.	Normas generales.

•	Usar nombres entendibles y descriptivos.

•	Uso de números: Con el fin de mejorar la lectura y entendimiento del código, se excluye el uso de números en el nombrado de cualquier variable, clases.

•	Uso de palabras reservadas: Con el fin de mejorar la lectura y entendimiento del código, se excluye el uso de palabras reservadas en nombres.

•	Longitud: Aunque un nombre puede ser del largo que el programador desee, debe tener una cantidad pequeña de caracteres para formar una variable corta regularmente 30 caracteres como máximo.

•	Todos los nombres de Clases, Variables, Objetos instanciados se deberán de escribir en idioma inglés.
      

#### 3.2.	Normas de nombrado de Variables 

• Notación de lowerCamelCase: es una convención de nomenclatura en la que un nombre está formado por múltiples palabras que se unen como una sola palabra con la primera letra de cada una de las múltiples palabras (excepto la primera) en mayúscula dentro de la nueva palabra que forma el nombre.

• Prefijos: únicamente utilizar con variables de tipo boolean, de lo contario son invalidas.

**Ejemplos de nombrado de variables**

*Validos*

![image](https://user-images.githubusercontent.com/112608548/228631173-793f3223-5a26-4821-bd2c-a59de43fbaf7.png)

![image](https://user-images.githubusercontent.com/112608548/228631248-f0c6d683-a6d1-44d3-a588-c7094f2b7bc5.png)

![image](https://user-images.githubusercontent.com/112608548/228631319-733f4a14-1be9-4980-9cb5-85d2f3309066.png)

*Invalidos*

![image](https://user-images.githubusercontent.com/112608548/228631385-67e75de5-97bf-4cf0-ab98-fa65943549bf.png)

![image](https://user-images.githubusercontent.com/112608548/228631451-b74163ac-c225-4932-be29-3ef6cba5ab28.png)

![image](https://user-images.githubusercontent.com/112608548/228631491-441226db-09ae-4703-92bc-b4a65dcd54ca.png)

![image](https://user-images.githubusercontent.com/112608548/228631534-13d60ec5-2211-4ad9-8b2a-0cae33e7e3e5.png)

![image](https://user-images.githubusercontent.com/112608548/228631577-2dceb4e8-9d9a-4f13-a207-faf2a9727229.png)

#### 3.3.	Normas de nombrado de Metodos

•	Cuidado con demasiadas líneas: un método contiene de 1 – 40 líneas. Se recomienda dividir un método complejo en varios más sencillos.

•	Notación de UpperCamelCase: para fin de apegarse al estándar de C# todos los nombres de métodos serán escritos bajo la notación de upperCamelCase.


**Ejemplos de nombrado de Metodos**

*Validos*

[![v2.png](https://i.postimg.cc/1R6CVVMP/v2.png)](https://postimg.cc/QV8kRtRn)

[![v3.png](https://i.postimg.cc/1zkdbGMC/v3.png)](https://postimg.cc/qtGQ3CPy)

*Invalidos*

[![i1.png](https://i.postimg.cc/wv9v5mJP/i1.png)](https://postimg.cc/d7SwwD5m)

[![i2.png](https://i.postimg.cc/d31Vpc5j/i2.png)](https://postimg.cc/Vrxw0pQJ)

[![i3.png](https://i.postimg.cc/GtQSBZRV/i3.png)](https://postimg.cc/m1PSJpty)

[![i4.png](https://i.postimg.cc/jdMFR0ZJ/i4.png)](https://postimg.cc/KRgN5VXZ)


**Excepciones en nombrado de Metodos**

• Existen métodos nombrados automáticamente al generar eventos para botones y sus respectivos clics; el nombrado por el IDE asigna un estilo Snake Case.

![image](https://user-images.githubusercontent.com/112608548/228633289-ad205e85-135e-496b-ae94-0a7465b6d2e7.png)

#### 3.4.	Normas de nombrado de Clases

• Aplican las normas de nombrado de los métodos.

•	Nombres en singular: Con el fin de mejorar la lectura y entendimiento del código, re recomienda que al momento de nombrar una clase se haga de forma singular.

**Ejemplos de nombrado de Clases**

*Validos*

[![v1.png](https://i.postimg.cc/mkcLLNCv/v1.png)](https://postimg.cc/9wh519xb)

*Invalidos*

![image](https://user-images.githubusercontent.com/112608548/228629835-c9b0fc41-e2c8-4f11-af0f-a73c8bde750c.png)

#### 3.5.	Normas de nombrado de Constantes

**Ejemplos de nombrado de Constantes**

*Validos*

*Invalidos*

### 4.	Estilo de código

#### 4.1.	Estilo de indentación

  Se utilizará el indentado por default que utiliza el IDE Visual Estudio 2022 solo para el código de C#, en cuanto al código en xaml si tendrá que ser indentado para un mayor orden y comprensión del código.
  
  #### 4.1.2.	Legibilidad
  
Un buen código ha de ser fácil de leer y tener una buena estructuración visual que acompañe a la lógica del algoritmo en cuestión. normalmente cada estándar define una serie de reglas sobre cómo han de posicionarse las llaves y uso de espacios.

Uso de espacios: cada línea tiene que estar bien separado para que se facilite la 	lectura y comprensión.
Colocación de llaves: deben estar en el mismo nivel que el código fuera de las 		llaves.


**Ejemplos de indentacion del código C# y xaml**

*Validos*

![image](https://user-images.githubusercontent.com/112608548/228636610-7273f14d-5edd-4c95-a7c0-7574130187ba.png)

![image](https://user-images.githubusercontent.com/112608548/228636876-4f93310a-c1a9-4fa5-936a-838684d3f208.png)


*Invalidos*

![image](https://user-images.githubusercontent.com/112608548/228636491-1019dae9-c13d-40a5-bcbe-8d04f5eedc1c.png)

![image](https://user-images.githubusercontent.com/112608548/228636513-92cd1ece-dd2c-416c-b9d5-27c9b9ad1dea.png)

![image](https://user-images.githubusercontent.com/112608548/228636539-80a7e064-80c8-424c-b6e4-3b543cf9bcbc.png)

![image](https://user-images.githubusercontent.com/112608548/228637316-cc9f8eec-afe0-4e33-bbeb-ea4095d53a9e.png)

![image](https://user-images.githubusercontent.com/112608548/228637681-8f16e3d2-3973-44cf-900b-55cecc31f975.png)

#### 4.2. Números mágicos

Los "números mágicos" en el código son valores literales utilizados directamente en el código en lugar de usar constantes con nombres descriptivos. Estos valores pueden ser enteros, flotantes o cualquier otro tipo de dato numérico.

El problema con el uso de números mágicos es que, si se usan de forma indiscriminada, pueden dificultar la comprensión y mantenimiento del código. Por ejemplo, si un desarrollador encuentra un número mágico en el código, puede ser difícil saber qué significa y cómo afecta el comportamiento del programa.

En lugar de usar números mágicos, es recomendable utilizar constantes con nombres significativos que se puedan reutilizar en diferentes partes del código, lo que facilita su mantenimiento y comprensión. Esto también hace que sea más fácil actualizar los valores numéricos en caso de que sea necesario hacer cambios en el código en el futuro.

**Ejemplos**

*Validos*

![image](https://user-images.githubusercontent.com/112608548/228638352-637378cc-fd90-40d9-a79a-d82dd0037f2a.png)

![image](https://user-images.githubusercontent.com/112608548/228638378-7ebae080-90ad-470b-bd3d-d3c8b93521e6.png)

![image](https://user-images.githubusercontent.com/112608548/228638406-8d45a1aa-6b14-4ad2-a27a-c68551cf3de9.png)

![image](https://user-images.githubusercontent.com/112608548/228638577-c3a42436-e0fd-402a-acbc-fb48ad68d811.png)


*Invalidos*

![image](https://user-images.githubusercontent.com/112608548/228638449-d9102a42-a6fb-4d78-bcd5-10c12f106fe5.png)

![image](https://user-images.githubusercontent.com/112608548/228638479-2b2f3652-44d9-4236-9af3-5a0c950ebe9b.png)

![image](https://user-images.githubusercontent.com/112608548/228638522-05431409-904b-4cfa-8900-bdfd4caae14b.png)

![image](https://user-images.githubusercontent.com/112608548/228638592-5c339443-4e8f-469c-af70-3a9b70ae4336.png)


#### 4.3.	Comentarios

Los comentarios en C# y en cualquier lenguaje de programación son una herramienta que sirve para apoyar la documentación de los programas que desarrollamos y así facilitar su posterior comprensión por parte de alguna otra persona que comprenda algo de C# o el lenguaje en particular. Por este motivo es que se tiene permitido agregar comentario, respetando una serie de normas.

•	Los comentarios no pueden ser muy largo máximo 50 caracteres.

•	Cada que se coloque un comentario para explicar una parte de código, se tiene que colocar arriba de la línea de código donde inicia y no al frente.

•	Evita tener líneas de código comentadas. Si el código ya no es útil mejor elimínalo.

### 5.	Justificación de prefijos.

**Se utilizan prefijos por las siguientes razones:**

• Claridad: Los prefijos pueden ayudar a clarificar el propósito o función de un componente. Por ejemplo, el prefijo "btn" en un botón indica que el componente es un botón, lo que puede ayudar a un desarrollador a entender su propósito sin tener que examinar todo el código.

• Consistencia: Los prefijos pueden ayudar a mantener la consistencia en el nombrado de componentes en un proyecto o código fuente. Al usar prefijos coherentes, se puede asegurar que todos los componentes estén claramente etiquetados y sean fáciles de encontrar.

• Evita conflictos: Los prefijos también pueden ayudar a evitar conflictos de nombres entre diferentes componentes. Si varios componentes comparten el mismo nombre, un prefijo puede diferenciarlos y evitar ambigüedades o confusiones.

• Facilita la navegación: Al utilizar prefijos significativos en los nombres de los componentes, la navegación dentro del código se hace más fácil y rápida, lo que puede ahorrar tiempo y esfuerzo en la búsqueda de un componente en particular.

**Ejemplos**

![image](https://user-images.githubusercontent.com/112608548/228638980-5bfe320d-3603-4064-b5c1-1164d0d432e7.png)


