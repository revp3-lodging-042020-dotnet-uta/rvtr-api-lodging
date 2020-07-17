using RVTR.Lodging.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests.Utilities
{
  public class Utilities
  {
    public static void InitializeDbForTests(LodgingContext db)
    {
      db.Messages.AddRange(GetSeedingMessages());
      db.SaveChanges();
    }

    public static void ReinitializeDbForTests(LodgingContext db)
    {
      db.Messages.RemoveRange(db.Messages);
      InitializeDbForTests(db);
    }

    public static List<Message> GetSeedingLodiging()
    {
      return new List<Message>()
    {
        new Message(){ Text = "TEST RECORD: You're standing on my scarf." },
        new Message(){ Text = "TEST RECORD: Would you like a jelly baby?" },
        new Message(){ Text = "TEST RECORD: To the rational mind, " +
            "nothing is inexplicable; only unexplained." }
    };
    }
  }
}
