using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;
using Produto.Application.Command.Commands;
using Produto.Application.Command.Validators;
using Produto.Application.Query.DAOs;

namespace Produto.Tests
{
    [TestFixture]
    public class AtualizarProdutoCommandTests
    {
        private Mock<IProdutoDao> _mockProdutoDao;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockProdutoDao = new Mock<IProdutoDao>();
        }

        [Test]
        public void Deve_Retornar_Erro_Quando_Id_Nome_Valor_Invalidos()
        {
            var command = new AtualizarProdutoCommand();
            var validator = new AtualizarProdutoCommandValidator(_mockProdutoDao.Object);
            var resultado = validator.TestValidate(command);

            resultado
                .ShouldHaveValidationErrorFor(command => command.Id)
                .WithErrorMessage("Id inválido.");

            resultado
                .ShouldHaveValidationErrorFor(command => command.Nome)
                .WithErrorMessage("Obrigatório informar o nome.");

            resultado
                .ShouldHaveValidationErrorFor(command => command.ValorVenda)
                .WithErrorMessage("Obrigatório informar o valor de venda.");

            resultado
                .ShouldNotHaveValidationErrorFor(command => command.Imagem);

            _mockProdutoDao.VerifyNoOtherCalls();
        }

        [Test]
        public void Deve_Executar_Com_Sucesso_Quando_Command_Valido()
        {
            var command = ProdutoFaker.AtualizarProdutoCommand.Generate();
            var validator = new AtualizarProdutoCommandValidator(_mockProdutoDao.Object);

            _mockProdutoDao.Setup(dao => dao.VerificarSeCadastradoAsync(It.IsAny<int>())).ReturnsAsync(true);

            var resultado = validator.TestValidate(command);

            resultado
                .ShouldNotHaveValidationErrorFor(command => command.Id);

            resultado
                .ShouldNotHaveValidationErrorFor(command => command.Nome);

            resultado
                .ShouldNotHaveValidationErrorFor(command => command.ValorVenda);

            resultado
                .ShouldNotHaveValidationErrorFor(command => command.Imagem);

            _mockProdutoDao.Verify(dao => dao.VerificarSeCadastradoAsync(It.IsAny<int>()), Times.Once);
            _mockProdutoDao.VerifyNoOtherCalls();
        }
    }
}
