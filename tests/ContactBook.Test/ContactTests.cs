using Xunit;
using ContactBook;

namespace ContactBook.Tests
{
    public class ContactTests
    {
        [Fact]
        public void Constructor_DebeAsignarPropiedadesCorrectamente()
        {
            // Arrange & Act
            var contacto = new Contact("Oscar", "Rodríguez", "7871234567", "oscar@mail.com");

            // Assert
            Assert.Equal("Oscar", contacto.Nombre);
            Assert.Equal("Rodríguez", contacto.Apellido);
            Assert.Equal("7871234567", contacto.Telefono);
            Assert.Equal("oscar@mail.com", contacto.Email);
        }

        [Fact]
        public void Equals_MismosValores_DebeRetornarTrue()
        {
            // Arrange
            var c1 = new Contact("Oscar", "G", "100", "test@test.com");
            var c2 = new Contact("Oscar", "G", "100", "test@test.com");

            // Act & Assert
            Assert.True(c1.Equals(c2));
            Assert.True(c1 == c2); // Prueba del operador sobrecargado
        }

        [Fact]
        public void Equals_ValoresDiferentes_DebeRetornarFalse()
        {
            // Arrange
            var c1 = new Contact("Oscar", "Rodríguez");
            var c2 = new Contact("Luis", "Acevedo");

            // Act & Assert
            Assert.False(c1.Equals(c2));
            Assert.True(c1 != c2); // Prueba del operador de desigualdad
        }

        [Fact]
        public void Equals_CompararConNulo_DebeRetornarFalse()
        {
            // Arrange
            var c1 = new Contact("Oscar");

            // Act & Assert
            Assert.False(c1.Equals(null));
            Assert.False(c1 == null);
        }

        [Fact]
        public void ToString_DebeFormatearCorrectamente()
        {
            // Arrange
            var contacto = new Contact("Oscar", "Marzan", "555", "oscar@mail.com");
            string esperado = "Oscar Marzan | Tel: 555 | Email: oscar@mail.com";

            // Act
            var resultado = contacto.ToString();

            // Assert
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void GetHashCode_MismosValores_DebenTenerMismoHash()
        {
            // Arrange
            var c1 = new Contact("Oscar", "G", "100", "test@test.com");
            var c2 = new Contact("Oscar", "G", "100", "test@test.com");

            // Act & Assert
            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }
    }
}