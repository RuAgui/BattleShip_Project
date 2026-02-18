Space Shooter - Learning Project 🚀


Este es un proyecto de desarrollo de videojuegos en Unity utilizado como plataforma de aprendizaje para implementar patrones de diseño y arquitecturas limpias en C#. El objetivo principal es aplicar los conceptos técnicos adquiridos durante mi formación actual.



🏗️ Arquitectura y Patrones Aplicados



1. Herencia y Polimorfismo
   Se ha implementado una jerarquía de clases para gestionar las entidades del juego de forma escalable:

BaseShip.cs: Clase base que encapsula la lógica común (salud y muerte) mediante el uso de modificadores protected y métodos virtual.

Especialización: Clases como Player.cs y Enemy.cs heredan de BaseShip, sobrescribiendo comportamientos específicos (como el método Die()) mediante override.



2. Encapsulamiento y Propiedades
   Uso de propiedades en C# para proteger el estado de los objetos y permitir lógica adicional durante la asignación:

Gestión de vida (Health) con validación de muerte automática.

Sistema de experiencia y cálculo de niveles dinámico en el Player.cs.



3. Separación de Responsabilidades (SoC)
   El comportamiento del jugador se ha dividido en componentes independientes para facilitar el mantenimiento:
   

PlayerMovement.cs: Gestión de físicas, fuerzas y rotaciones suavizadas (Quaternion.Slerp).

PlayerCombat.cs: Sistema de armamento independiente que gestiona la cadencia de tiro (fireRate) y puntos de disparo múltiples.



🚀 Funcionalidades Implementadas


Sistema de Vuelo Avanzado: Movimiento basado en físicas con inclinación lateral (tilt) y aceleración suavizada.

Combate Dual: Sistema de disparo sincronizado desde múltiples puntos (firePoint) utilizando colecciones (Arrays) y bucles foreach.

Lógica de Proyectiles: Balas físicas que utilizan Rigidbody.linearVelocity y gestión de tiempo de vida para optimización de memoria.



🛠️ Tecnologías


Motor: Unity 6.

Lenguaje: C#.

Input System: Implementación del nuevo sistema de eventos de Unity para el control del jugador.

