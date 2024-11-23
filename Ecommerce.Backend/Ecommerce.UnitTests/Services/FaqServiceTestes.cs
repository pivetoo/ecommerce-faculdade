using Ecommerce.Application.Services;
using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Moq;

namespace Ecommerce.UnitTests.Services
{
    public class FaqServiceTestes
    {
        private Mock<IFaqRepository> _faqRepositoryMock;
        private FaqService _faqService;

        [SetUp]
        public void SetUp()
        {
            _faqRepositoryMock = new Mock<IFaqRepository>();
            _faqService = new FaqService(_faqRepositoryMock.Object);
        }

        [Test]
        public async Task CriarFAQ()
        {
            var faq = new FAQ { Id = 1, Titulo = "Nova FAQ", Descricao = "Nova Descricao" };
            _faqRepositoryMock.Setup(repo => repo.Incluir(It.IsAny<FAQ>())).Returns(Task.CompletedTask);

            var result = await _faqService.CriarFAQ(faq);

            _faqRepositoryMock.Verify(repo => repo.Incluir(faq), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AtualizarFAQ()
        {
            var existingFaq = new FAQ { Id = 1, Titulo = "FAQ Existente", Descricao = "Descricao Existente" };
            var updatedFaq = new FAQ { Id = 1, Titulo = "Titulo Atualizado", Descricao = "Descricao Atualizada" };

            _faqRepositoryMock.Setup(repo => repo.Retornar(existingFaq.Id)).ReturnsAsync(existingFaq);
            _faqRepositoryMock.Setup(repo => repo.Atualizar(It.IsAny<FAQ>())).Returns(Task.CompletedTask);

            var result = await _faqService.AtualizarFAQ(updatedFaq);

            _faqRepositoryMock.Verify(repo => repo.Atualizar(It.Is<FAQ>(f => f.Titulo == "Titulo Atualizado" && f.Descricao == "Descricao Atualizada")), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void AtualizarFAQ_Exception()
        {
            _faqRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((FAQ)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _faqService.AtualizarFAQ(new FAQ { Id = 999 }));
        }

        [Test]
        public async Task ConsultarFAQPorId()
        {
            var faq = new FAQ { Id = 1, Titulo = "FAQ 1", Descricao = "Descricao 1" };
            _faqRepositoryMock.Setup(repo => repo.Retornar(faq.Id)).ReturnsAsync(faq);

            var result = await _faqService.ConsultarFAQPorId(faq.Id);

            Assert.AreEqual(faq, result);
        }

        [Test]
        public void ConsultarFAQPorId_Exception()
        {
            _faqRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((FAQ)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _faqService.ConsultarFAQPorId(999));
        }

        [Test]
        public async Task ExcluirFAQ()
        {
            var faq = new FAQ { Id = 1 };
            _faqRepositoryMock.Setup(repo => repo.Retornar(faq.Id)).ReturnsAsync(faq);
            _faqRepositoryMock.Setup(repo => repo.Excluir(faq.Id)).Returns(Task.CompletedTask);

            var result = await _faqService.ExcluirFAQ(faq.Id);

            _faqRepositoryMock.Verify(repo => repo.Excluir(faq.Id), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void ExcluirFAQ_Exception()
        {
            _faqRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((FAQ)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _faqService.ExcluirFAQ(999));
        }

        [Test]
        public async Task ContarFAQs()
        {
            _faqRepositoryMock.Setup(repo => repo.ContarFAQs()).ReturnsAsync(5);

            var result = await _faqService.ContarFAQs();

            Assert.AreEqual(5, result);
        }
    }
}
