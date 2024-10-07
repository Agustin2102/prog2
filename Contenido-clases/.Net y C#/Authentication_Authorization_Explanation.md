
# Autenticación y Autorización: Definición y Diferencias

Cuando hablamos de seguridad en aplicaciones web, especialmente en APIs, es fundamental entender la diferencia entre **autenticación** y **autorización**, ya que ambos son conceptos clave en cualquier sistema que maneje usuarios y permisos.

## 1. Autenticación

La **autenticación** responde a la pregunta: **¿Quién eres?**

- Es el proceso mediante el cual el sistema verifica la **identidad** de un usuario.
- En una API, la autenticación generalmente se realiza mediante el envío de credenciales (nombre de usuario y contraseña) al servidor.
- Una vez que el usuario es autenticado, el sistema genera un **token** (como un JWT), que el cliente usará en solicitudes posteriores para demostrar su identidad.
- **Ejemplo**: Cuando un usuario inicia sesión, el sistema valida sus credenciales (nombre de usuario y contraseña) y confirma que es quien dice ser.

## 2. Autorización

La **autorización** responde a la pregunta: **¿Qué tienes permiso para hacer?**

- Es el proceso que determina si un usuario autenticado tiene **permisos** para acceder a ciertos recursos o realizar ciertas acciones.
- Una vez que el usuario está autenticado, el sistema verifica si tiene los permisos necesarios para acceder a los recursos solicitados (por ejemplo, solo los administradores pueden acceder a ciertos datos o funcionalidades).
- **Ejemplo**: Un usuario autenticado puede estar autorizado para ver su perfil, pero no para modificar los datos de otros usuarios, a menos que tenga permisos de administrador.

## Diferencias Clave

| Aspecto         | Autenticación                            | Autorización                         |
|-----------------|------------------------------------------|--------------------------------------|
| ¿Qué verifica?   | Verifica la **identidad** del usuario.   | Verifica los **permisos** del usuario. |
| Cuándo ocurre   | Ocurre antes de la autorización.          | Ocurre después de la autenticación.   |
| Resultado       | Si es correcta, el sistema confirma quién es el usuario. | Si es correcta, el sistema permite o deniega acceso a ciertos recursos. |

## Ejemplo práctico

1. **Autenticación**: Un usuario envía su nombre de usuario y contraseña para iniciar sesión. El sistema valida esas credenciales y genera un **token JWT** si son correctas.
   
2. **Autorización**: Cuando el usuario intenta acceder a un recurso, como un listado de usuarios o una sección administrativa, el sistema verifica si ese usuario tiene los permisos necesarios basándose en su **rol** (por ejemplo, administrador).
