namespace MVCPrototipo.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        //Aqui en grado yo haria otra entidad llamada grado. Por que? Porque asi evito que alguien digite 2do B o 2doV o 2b para
        //referirse al mismo grado. Si tengo un listado de los grados limito al usuario a que escoja desde mi listado
        public int Grado { get; set; }
        public byte[] FotoPerfil { get; set; }
    }
}