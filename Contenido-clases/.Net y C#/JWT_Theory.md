
# JSON Web Tokens (JWT): Teoría y Aplicación

## ¿Qué es JWT (JSON Web Token)?

Un **JWT (JSON Web Token)** es un estándar abierto que se utiliza para transmitir información de manera segura entre dos partes (cliente y servidor) en formato **JSON**. Se usa comúnmente en aplicaciones web, especialmente para la autenticación en **APIs**. Un JWT es un token **autocontenido**, lo que significa que incluye toda la información necesaria para la autenticación y autorización sin necesidad de mantener sesiones en el servidor.

### Características principales de un JWT:
- **Autocontenido**: Toda la información necesaria está en el token.
- **Seguro**: La firma del token garantiza que no ha sido alterado.
- **Compacto**: Utiliza codificación Base64, por lo que es ligero y fácil de transportar en una URL o en el encabezado de una solicitud HTTP.

## Estructura de un JWT

Un JWT tiene tres partes, separadas por puntos (`.`):

```
Header.Payload.Signature
```

Estas tres partes se codifican en **Base64** y se separan con puntos:

### 1. **Header** (Encabezado)

El encabezado contiene información sobre cómo se ha firmado el token. Es un objeto JSON que típicamente tiene dos campos:
- `alg`: El algoritmo de firma (por ejemplo, **HS256** para HMAC SHA256).
- `typ`: El tipo de token, que es `JWT`.

Ejemplo de un **Header** codificado en Base64:

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

### 2. **Payload** (Cuerpo)

El cuerpo del JWT contiene los **claims**, que son las afirmaciones sobre la identidad del usuario y otros metadatos. Existen diferentes tipos de claims:
- **Claims registrados**: Campos estándar como `sub` (subject), `iat` (issued at), `exp` (expiration), etc.
- **Claims personalizados**: Información que podemos agregar, como el rol del usuario.

Ejemplo de un **Payload** en JSON:

```json
{
  "sub": "1234567890",
  "name": "Jon Doe",
  "admin": true,
  "iat": 1516239022
}
```

### 3. **Signature** (Firma)

La firma se usa para garantizar que el JWT no ha sido alterado. Se crea combinando el header y el payload, y luego cifrándolos con un secreto conocido por el servidor.

Ejemplo de cómo se genera la firma en HMAC SHA256:

```
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret
)
```

Si alguien altera el token, la firma no coincidirá, por lo que se invalidará el JWT.

## Utilidades de JWT

1. **Autenticación sin estado (stateless)**: JWT permite autenticar usuarios sin mantener sesiones en el servidor. Esto es especialmente útil en aplicaciones modernas que escalan fácilmente, como las **APIs REST**.
   
2. **Portabilidad**: Los tokens JWT se pueden usar en múltiples lenguajes y plataformas, lo que los hace muy flexibles.

3. **Seguridad**: Aunque el contenido de un JWT no está encriptado, está **firmado**. Esto asegura que el token no haya sido alterado. Opcionalmente, el token también puede ser encriptado (JWE) para añadir más seguridad.

## Flujo básico de JWT en una API

### 1. **Inicio de sesión (Login)**

El cliente envía sus credenciales (nombre de usuario y contraseña) al servidor para iniciar sesión.

### 2. **Generación del Token**

Si las credenciales son correctas, el servidor genera un JWT que contiene información sobre el usuario y lo envía al cliente.

### 3. **Uso del Token**

El cliente guarda el token (usualmente en el almacenamiento local o en una cookie segura) y lo envía en cada solicitud posterior en el encabezado HTTP de la siguiente forma:

```
Authorization: Bearer <token>
```

### 4. **Validación del Token**

El servidor valida el token verificando:
- Que la firma es válida y no ha sido alterada.
- Que el token no ha expirado.
- Que el emisor y la audiencia coinciden con los valores esperados.

Si el token es válido, el servidor otorga acceso al usuario.

## Ejemplo de Token JWT

Este es un ejemplo completo de un JWT con las tres partes (Header, Payload, y Signature):

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvbiBEb2UiLCJhZG1pbiI6dHJ1ZX0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

Este JWT tiene la siguiente estructura:

- **Header**: 
   ```json
   {
     "alg": "HS256",
     "typ": "JWT"
   }
   ```

- **Payload**:
   ```json
   {
     "sub": "1234567890",
     "name": "Jon Doe",
     "admin": true
   }
   ```

- **Signature**: Firmada con el algoritmo HS256 usando una clave secreta conocida por el servidor.

## Beneficios de usar JWT

- **No requiere almacenamiento en el servidor**: Una vez emitido el token, el servidor no necesita almacenarlo o gestionarlo (sin estado).
- **Escalabilidad**: Ideal para aplicaciones distribuidas donde múltiples servidores necesitan autenticar solicitudes sin compartir una base de datos de sesiones.
- **Interoperabilidad**: Los JWT son fácilmente entendibles y usables en distintos lenguajes y plataformas.

## Seguridad en JWT

Aunque un JWT no está cifrado por defecto, la firma garantiza la **integridad del token**. Para una mayor seguridad:
- Usa HTTPS para evitar la intercepción del token.
- Asegúrate de que los tokens tengan un **tiempo de expiración** corto.
- Revoca los tokens comprometidos usando una lista de revocación de tokens (aunque esto requiere estado en el servidor).

