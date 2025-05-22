using ProgettoApi.models;
using System;

namespace ProgettoApi.Service
{
    public class ParkingService
    {
        private List<OutputDati> _irregolari = new List<OutputDati>();
        private List<InputDatiTest> cliente = new List<InputDatiTest>();

        public string Entry(ProgettoApi.models.InputDatiTest input)
        {
            try
            {
                // Verifica Targa
                if ((string.IsNullOrWhiteSpace(input.Plate) || input.Data == null) || (string.IsNullOrWhiteSpace(input.Plate) && input.Data == null))
                {
                    throw new ArgumentException("La targa può essere nulla e/o la data può essere non valida");
                }

                // Se tutto va bene
                Console.WriteLine("Targa valida!");
                cliente.Add(input);
                return "ENTRATO";
            }
            catch (Exception ex)
            {
                return "Si è verificato un errore: " + ex.Message + ". L'accesso è stato negato.";
            }

        }

        [HttpPost]
        public IActionResult Exit([FromBody] OutputDati output)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(output.PlateO))
                {
                    throw new ArgumentException("La targa non può essere vuota.");
                }
                Console.WriteLine("Targa valida! Adesso verificherò che la targa sia presente nel registro degli ingressi e ti farò sapere.");
                foreach (var a in cliente)
                {
                    if (output.PlateO == cliente.Plate)
                    {
                        try
                        {

                            if (output.DataO < cliente.Data)
                            {
                                throw new ArgumentException("L'orario di uscita non può essere precedente all'orario di entrata.");
                            }else
                            {
                                TimeSpan durata = output.DataO - cliente.Data;
                                if (durata.TotalHours > 2)
                                {
                                    _irregolari.Add(output.Plate0);
                                    _irregolari.Add(output.DataO);
                                    output.contatore = contatore + 1;
                                    _irregolari.Add(output.contatore);
                                }
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine("Errore di validazione: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Errore imprevisto: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore generale: " + ex.Message);
            }
        }
    }
}