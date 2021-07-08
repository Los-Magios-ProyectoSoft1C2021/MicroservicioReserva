namespace Template.Domain.DTOs.HttpResponse
{
    public class ResponseHabitacionDto
    {
        public int HabitacionId { get; set; }
        public int HotelId { get; set; }
        public string Nombre { get; set; }
        public ResponseCategoriaDto Categoria { get; set; }
    }

    public class ResponseCategoriaDto
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
