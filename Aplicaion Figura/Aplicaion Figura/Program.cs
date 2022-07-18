//IMPORTO DLL DE MI LIBRERIA
using LibreriaFiguras;
using Newtonsoft.Json;

namespace AplicacionFiguras
{
    public class Program
    {
        private static string _path = @"C:\data.json";
        

        public static void Main(string[] args)
        {
            //LEER JSON
            Console.WriteLine("LEER JSON -> FIGURAS");
            var figuras = GetFiguresJsonFromFile();
            Console.WriteLine(figuras);

            Console.WriteLine();
            Console.WriteLine("Defino Creadores y Productos");
            CreadorRectangulo creatorA = new();
            CreadorCirculo creatorB = new();
            CreadorTriangulo creatorC = new();
            //CreadorTrapecio creatorD = new();

            Console.WriteLine();
            Console.WriteLine("*********Creadores*********");
            List<IFigura> listaFiguras = new List<IFigura>();

            var fig = JsonConvert.DeserializeObject<List<AplicacionFigura.Modelo>>(figuras);

            if (fig != null)
            {
                for (int i = 0; i < fig.Count; i++)
                {
                    Console.WriteLine("Figura: " + i);
                    if (fig[i].Figura.Nombre == "Rectángulo")
                    {
                        listaFiguras.Add(creatorA.crearFigura(fig[i].Figura.Nombre, fig[i].Figura.Tipo, fig[i].Figura.Cadena));
                    }
                    else if (fig[i].Figura.Nombre == "Circulo")
                    {
                        listaFiguras.Add(creatorB.crearFigura(fig[i].Figura.Nombre, fig[i].Figura.Tipo, fig[i].Figura.Cadena));
                    }
                    else if (fig[i].Figura.Nombre == "Triángulo")
                    {
                        listaFiguras.Add(creatorC.crearFigura(fig[i].Figura.Nombre, fig[i].Figura.Tipo, fig[i].Figura.Cadena));
                    }
                    
                }
            }

            double acumuladorAreas = 0;
            double canvaArea = 100;
            Console.WriteLine();
            Console.WriteLine("*********FIGURAS GEOMETRICAS*******");
            int cont = 0;
            foreach (IFigura figura in listaFiguras)
            {
                Console.WriteLine("FIGURA: " + cont);
                figura.dibujarFigura();
                double area = figura.calcularArea();
                Console.WriteLine("El Área es: " + area);
                acumuladorAreas = acumuladorAreas + area;
                Console.WriteLine("El longitud de la etiqueta es: " + figura.calcularLongitudEtiqueta());
                Console.WriteLine("==========================");
                cont++;
            }

            Console.WriteLine();
            Console.WriteLine("AREA TOTAL DE CANVA  : " + canvaArea);
            Console.WriteLine();
            Console.WriteLine("AREA TOTAL DE FIGURAS: " + acumuladorAreas);
            Console.WriteLine();
            Console.WriteLine("***CANVA***");
            if (canvaArea > acumuladorAreas)
            {
                foreach (IFigura figu in listaFiguras)
                {
                    Console.WriteLine();
                    Console.WriteLine("*****DIBUJANDO EN CANVA****");
                    figu.dibujarFigura();
                }
            }
            else
            {
                Console.WriteLine("ÁREA DE CANVA INSUFICIENTE");
            }
        }

        public static string GetFiguresJsonFromFile()
        {
            string figurasFromFile;
            using (var render = new StreamReader(_path))
            {
                figurasFromFile = render.ReadToEnd();
            }
            return figurasFromFile;
        }

    }
}