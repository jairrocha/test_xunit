using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arranger

            var valorNegativo = -100;

            //Assert
            Assert.Throws<ArgumentException>
                (
                    //Act
                    () => new Lance(null, valorNegativo)
                );

        }
    }
}
