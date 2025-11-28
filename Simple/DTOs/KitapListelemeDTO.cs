namespace Simple.DTOs
{
    public class KitapListelemeDTO
    {
        public int Id { get; set; } // ID ile bulmak isteriz tabii

        //gerekli olan herşey Ekleme classından copy paste atıcam
        public string Ad { get; set; }
        public string Yazar { get; set; }
        public int SayfaSayisi { get; set; }

        //Listelerken dbdeki görüntülenmesini istemediğim şeyleri yazmıyorum...


    }
}
