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
using System.Windows.Media.Imaging;

namespace Domain.BelongingCreation
{
    public class Belonging
    {
        public string Category { get; set; }
        public string GenericDescription { get; set; }
        public string Features { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public int ApraisalAmount { get; set; }
        public int LoanAmount { get; set; }
        public float PorcentLoan { get; set; }

        public List<BitmapImage> imagesBitmap = new List<BitmapImage>();
        public List<byte[]> imagesBytes = new List<byte[]>();
    }
}
