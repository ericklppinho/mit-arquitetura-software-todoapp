namespace ToDo.Domain.Entities
{

    public class Item
	{

		public Item() { }

		public Item(string description)
		{
			Id = Guid.NewGuid();
			Description = description;
            Done = false;
			CreatedAt = DateTime.Now;
		}

        public Item(Guid id, string description, bool done)
        {
            Id = id;
            Description = description;
            Done = done;
        }

        public Guid Id { get; set; }

		public string Description { get; set; }

		public bool Done { get; set; }

		public DateTime CreatedAt { get; private set; }
		
		public void MarkAsDone() => Done = true;

        public void MarkAsUnDone() => Done = false;

    }

}

