namespace ContactBook
{
    public class Contact : IEquatable<Contact>
    {
        private string nombre;
        private string apellido;
        private string telefono;
        private string email;

        public Contact(string nombre = "", string apellido = "", string telefono = "", string email = "")
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
        }
        
        public string Nombre 
        { 
            get => nombre; 
            set => nombre = value; 
        }

        public string Apellido 
        { 
            get => apellido; 
            set => apellido = value; 
        }

        public string Telefono 
        { 
            get => telefono; 
            set => telefono = value; 
        }

        public string Email 
        { 
            get => email; 
            set => email = value; 
        }

        public override string ToString() => $"{nombre} {apellido} | Tel: {telefono} | Email: {email}";

        public bool Equals(Contact? otro)
        {
            if (otro is null) return false;
            if (ReferenceEquals(this, otro)) return true;

            return nombre == otro.nombre && 
                   apellido == otro.apellido && 
                   telefono == otro.telefono && 
                   email == otro.email;
        }

        public override bool Equals(object? obj) => Equals(obj as Contact);

        public override int GetHashCode() => HashCode.Combine(nombre, apellido, telefono, email);

        public static bool operator ==(Contact? x, Contact? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.Equals(y);
        }

        public static bool operator !=(Contact? x, Contact? y) => !(x == y);
    }
}