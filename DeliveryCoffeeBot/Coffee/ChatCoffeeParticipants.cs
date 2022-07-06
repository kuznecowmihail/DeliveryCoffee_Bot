using System;
using System.Collections.Generic;

namespace DeliveryCoffeeBot.Coffee
{
    public static class ChatCoffeeParticipants
    {
        public static Dictionary<long, CoffeeParticipants> Participants { get; set; }
            = new Dictionary<long, CoffeeParticipants>();
    }

    public class CoffeeParticipants
    {
        public List<Participant> Participants { get; set; }
        public DateTime Date { get; set; }
        public bool IsUsed { get; set; }

        public CoffeeParticipants()
        {
            Participants = new List<Participant>();
            Date = DateTime.Now;
        }
    }

    public class Participant
    {
        public string UserName { get; set; }
        public long Id { get; set; }

        public Participant(string userName, long id)
        {
            UserName = userName;
            Id = id;
        }
    }
}
