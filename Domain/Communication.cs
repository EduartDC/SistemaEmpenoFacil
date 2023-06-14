using Domain.BelongingCreation;

using System.Collections.Generic;
using System.Windows.Media.Imaging;


namespace Domain.Communitation
{
    public interface Communication
    {
        //comunicacion entre CU06Crear registro prendario y CU02Crear contratos
        void refreshBelongings(List<Belonging> belongingsList);

        void refreshArticles();

    }
}
