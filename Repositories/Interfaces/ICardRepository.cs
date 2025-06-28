using CardCollector_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Repositories.Interfaces;

public interface ICardRepository : IRepositoryCrud<Card>
{
}