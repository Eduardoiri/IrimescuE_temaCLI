1. Ce este un viewport?
Un viewport este regiunea rectangulară din fereastra de afișare unde se redă scena, definită prin poziție și dimensiuni în pixeli.

2. Ce reprezintă conceptul de frames per seconds în OpenGL?
Frames per second (FPS) indică numărul de cadre redată într-o secundă, influențând fluiditatea animației.

3. Când este rulată metoda OnUpdateFrame()?
OnUpdateFrame() este rulată la fiecare cadru pentru a actualiza logica aplicației, cum ar fi mișcarea și interacțiunile.

4. Ce este modul imediat de randare?
Modul imediat de randare permite desenarea graficelor prin comenzi directe (glBegin, glEnd), dar este ineficient pentru aplicații complexe.

5. Care este ultima versiune de OpenGL care acceptă modul imediat?
Ultima versiune care suportă modul imediat este OpenGL 2.1; versiunile ulterioare au depreciat acest mod.

6. Când este rulată metoda OnRenderFrame()?
OnRenderFrame() este rulată după actualizarea stării aplicației, pentru a reda efectiv scena.

7. De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
OnResize() ajustează viewport-ul și proiecția pentru a asigura o redare corectă a obiectelor când fereastra este redimensionată.

8. Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView()?
- fov: Unghiul câmpului vizual (între 0 și π).
- aspectRatio: Raportul lățime/înălțime al viewport-ului.
- near: Distanța la planul de tăiere apropiat (număr pozitiv).
- far: Distanța la planul de tăiere îndepărtat (număr pozitiv, mai mare decât near).

Observație pr1
Când modifici valoarea constantei „MatrixMode.Projection”, poți observa că schimbările afectează cum este redată scena în funcție de tipul de proiecție utilizat (perspectivă sau ortografică).
