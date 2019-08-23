namespace IT
{
    public class Xpert
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string NumPhone { get; set; }
        
        public Xpert(uint _id)
        {
            Id = _id;
        }
    }
}