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
            CreadorRectangulo creatorRectangulo = new CreadorRectangulo();
            CreadorCirculo creatorCirculo = new CreadorCirculo();
            CreadorTriangulo creatorTriangulo = new CreadorTriangulo();

            Console.WriteLine("\n***Creadores***");
            List<IFigura> listaFiguras = new List<IFigura>();

            var figuraFile = JsonConvert.DeserializeObject<List<AplicacionFigura.Modelo>>(figuras);

            if (figuraFile != null)
            {
                for (int i = 0; i < figuraFile.Count; i++)
                {
                    Console.WriteLine("Figura: " + i);
                    if (figuraFile[i].Figura.Nombre == "Rectángulo")
                    {
                        listaFiguras.Add(creatorRectangulo.crearFigura(figuraFile[i].Figura.Nombre, figuraFile[i].Figura.Tipo, figuraFile[i].Figura.Cadena));
                    }
                    else if (figuraFile[i].Figura.Nombre == "Circulo")
                    {
                        listaFiguras.Add(creatorCirculo.crearFigura(figuraFile[i].Figura.Nombre, figuraFile[i].Figura.Tipo, figuraFile[i].Figura.Cadena));
                    }
                    else if (figuraFile[i].Figura.Nombre == "Triángulo")
                    {
                        listaFiguras.Add(creatorTriangulo.crearFigura(figuraFile[i].Figura.Nombre, figuraFile[i].Figura.Tipo, figuraFile[i].Figura.Cadena));
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
                Console.WriteLine("La longitud de la etiqueta es: " + figura.calcularLongitudEtiqueta());
                Console.WriteLine("==========================\n");
                cont++;
            }

            Console.WriteLine("\nAREA TOTAL DE CANVA  : " + canvaArea);
            Console.WriteLine("AREA TOTAL DE FIGURAS: " + acumuladorAreas);
            Console.WriteLine();
            Console.WriteLine("\n***CANVA***");
            if (canvaArea > acumuladorAreas)
            {
                foreach (IFigura figura in listaFiguras)
                {
                    Console.WriteLine();
                    Console.WriteLine("***DIBUJANDO EN CANVA***");
                    figura.dibujarFigura();
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