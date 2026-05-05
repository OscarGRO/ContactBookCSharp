namespace ContactBook;

public class ContactComparer : IComparer<Contact>
{
    public enum TipoOrden
    {
        Nombre,
        Apellido,
        Telefono,
        Email
    };

    private TipoOrden tipoOrden;

    public ContactComparer(TipoOrden tipoOrden)
    {
        SetTipoOrden(tipoOrden);
    }

    public TipoOrden GetTipoOrden()
    {
        return tipoOrden;
    }

    public void SetTipoOrden(TipoOrden tipoOrden)
    {
        this.tipoOrden = tipoOrden;
    }

    public int Compare(Contact? x, Contact? y)
    {
        int r = 0;
        switch (tipoOrden)
        {
            case TipoOrden.Apellido:
                return (r = string.Compare(x?.Apellido, y?.Apellido)) != 0 ? r :
                       (r = string.Compare(x?.Nombre, y?.Nombre)) != 0 ? r :
                       (r = string.Compare(x?.Telefono, y?.Telefono)) != 0 ? r :
                       string.Compare(x?.Email, y?.Email);

            case TipoOrden.Telefono:
                return (r = string.Compare(x?.Telefono, y?.Telefono)) != 0 ? r :
                       (r = string.Compare(x?.Nombre, y?.Nombre)) != 0 ? r :
                       (r = string.Compare(x?.Apellido, y?.Apellido)) != 0 ? r :
                       string.Compare(x?.Email, y?.Email);

            case TipoOrden.Email:
                return (r = string.Compare(x?.Email, y?.Email)) != 0 ? r :
                       (r = string.Compare(x?.Nombre, y?.Nombre)) != 0 ? r :
                       (r = string.Compare(x?.Apellido, y?.Apellido)) != 0 ? r :
                       string.Compare(x?.Telefono, y?.Telefono);

            case TipoOrden.Nombre:
            default:
                return (r = string.Compare(x?.Nombre, y?.Nombre)) != 0 ? r :
                       (r = string.Compare(x?.Apellido, y?.Apellido)) != 0 ? r :
                       (r = string.Compare(x?.Telefono, y?.Telefono)) != 0 ? r :
                       string.Compare(x?.Email, y?.Email);
        }
    }
}