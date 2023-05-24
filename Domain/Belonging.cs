/*
 * Fecha 06/04/2023
 * Autor:Jonathan Hernández
 * Descripcion: Clase usada en CU06 crear registro prendario
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Domain.BelongingCreation
{
    public class Belonging
    {
        public int idBelonging { get; set; }
        public string Category { get; set; }
        public string GenericDescription { get; set; }
        public string Features { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public double ApraisalAmount { get; set; }
        public double LoanAmount { get; set; }
        public float PorcentLoan { get; set; }
        public int Contract_idConctract { get; set; }
        public DateTime DeadLine { get; set; }
        public String State { get; set; }

        public byte[] image { get; set; }

        public ImageSource imageConverted { get; set; }

        public List<BitmapImage> imagesBitmap = new List<BitmapImage>();
        public List<byte[]> imagesBytes = new List<byte[]>();
    }
}
