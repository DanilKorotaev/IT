﻿namespace IT
{
    public class Question
    {
        public uint Id { get; set; }
        public string Discription { get; set; }
        // public
        public Question(uint _id)
        {
            Id = _id;
        }

        public Question(uint _id,string _discription)
        {
            Id = _id;
            Discription = _discription;
        }
    }
}