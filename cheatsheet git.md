# Git Cheat Sheet para Gestión de Código en Clase

 ## Menú
 - [Estructura de Ramas](#estructura-de-ramas)
 - [Configuración Inicial](#configuración-inicial)
 - [Mantener tu Rama Actualizada](#mantener-tu-rama-actualizada)
 - [Trabajar en Cambios](#trabajar-en-cambios)
 - [Uso de Stash](#uso-de-stash)
 - [Git workflow](#git-workflow)
 - [Material complementario](#material-complementario)
 - [Notas Adicionales](#notas-adicionales)

 ## Estructura de Ramas
 1. **Rama `main`**:
    - Contiene el código más actualizado.
    - Se actualiza al final de cada clase.

 2. **Rama del Estudiante (`ariel.romero`, `guille.cortez`, etc.)**:
    - Rama personal de cada estudiante para hacer cambios y pruebas.

 ## Configuración Inicial
 ```bash
  git clone <URL_DEL_REPOSITORIO>  Clona el repositorio a tu máquina local
  cd <NOMBRE_DEL_REPOSITORIO>  Cambia el directorio a la carpeta del repositorio
  git checkout -b nombre.apellido   Crea y cambia a una nueva rama con tu nombre
  git push -u origin nombre.apellido  Sube la nueva rama al repositorio remoto
 ```

 ## Mantener tu Rama Actualizada
 1. **Obtener los Últimos Cambios de `main`**
 ```bash
  git checkout main  Cambia a la rama main
  git pull origin main  Obtiene y fusiona los últimos cambios de la rama main
 ```

 2. **Actualizar tu Rama Personal con Cambios de `main`**
 ```bash
  git checkout nombre.apellido   Cambia a tu rama personal
  git fetch origin  Obtiene las últimas referencias del repositorio remoto
  git merge main   Fusiona los cambios de la rama main en tu rama personal
 ```

 **Resolver Conflictos durante el Merge:**
 - **Resolver Conflictos:** Edita los archivos para resolver los conflictos.
 - **Añadir Archivos Corregidos:**
 ```bash
  git add <archivo>  Agrega los archivos con los conflictos resueltos
 ```
 - **Confirmar el Merge:**
 ```bash
  git commit -m "Resolución de conflictos y merge"  Confirma el merge con un mensaje
 ```

 3. **Subir los Cambios después del Merge**
 ```bash
  git push origin nombre.apellido  Sube los cambios fusionados a tu rama en el repositorio remoto
 ```

 ## Trabajar en Cambios
 1. **Hacer Cambios y Confirmarlos**
 ```bash
  git add .  Agrega todos los cambios en el directorio actual
  git commit -m "Descripción de los cambios"  Confirma los cambios con un mensaje
 ```

 2. **Subir Cambios a tu Rama**
 ```bash
  git push origin nombre.apellido  Sube los cambios a tu rama en el repositorio remoto
 ```

 ## Uso de Stash
 `stash` te permite guardar cambios temporales que no deseas confirmar todavía.

 1. **Guardar Cambios en el Stash**
 ```bash
  git stash save "Descripción"  Guarda tus cambios no confirmados en el stash
 ```

 2. **Ver los Cambios Guardados en el Stash**
 ```bash
  git stash list  Muestra la lista de stashes guardados
 ```

 3. **Aplicar Cambios del Stash al Directorio de Trabajo**
 ```bash
  git stash apply id  Aplica los cambios guardados según el <id> seleccionado
 ```

 4. **Eliminar un Stash Aplicado**
 ```bash
  git stash drop id  Elimina un stash de la lista según el <id> seleccionado
 ```

 5. **Eliminar Todos los Stashes**
 ```bash
  git stash clear  Elimina todos los stashes guardados
 ```

## Git Workflow

![Git-Workflow](git-workflow1.png)

## Material complementario

   Información complementaria para el uso de Git.
   - https://www.diegocmartin.com/tutorial-git/
   - https://training.github.com/downloads/es_ES/github-git-cheat-sheet.pdf
   - https://www.youtube.com/watch?v=jGehuhFhtnE

## Notas Adicionales
 - **Siempre hacé un pull o fetch antes de merge** para asegurarte de que estás trabajando con la versión más reciente del código.
 - **Hacé un backup de tus cambios** antes de realizar operaciones avanzadas como merge puede ser una buena práctica.
