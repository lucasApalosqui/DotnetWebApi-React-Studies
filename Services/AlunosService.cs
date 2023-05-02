using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AlunosApi.Services
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunosService(AppDbContext context)
        {
            _context = context;
        }

        //Get all alunos
        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Get Students", ex);
            }
        }

        // Get Aluno By Especific Id
        public async Task<Aluno> GetAluno(int Id)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(Id);
                return aluno;

            }
            catch (Exception ex)
            {
                throw new Exception("Error get Student By Id", ex);
            }
        }

        // Find Aluno By Name
        public async Task<IEnumerable<Aluno>> GetAlunoByName(string name)
        {
            try
            {
                IEnumerable<Aluno> alunos;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    alunos = await _context.Alunos.Where(n => n.Name.Contains(name)).ToListAsync();
                }
                else
                {
                    alunos = await GetAlunos();
                }
                return alunos;


            }
            catch (Exception ex)
            {
                throw new Exception("error find Student by Name", ex);
            }
        }

        // Create Aluno
        public async Task CreateAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Trata exceção do Entity Framework
                throw new Exception("Error creating student in the database.", ex);
            }
            catch (Exception ex)
            {
                // Trata outras exceções
                throw new Exception("Error creating student", ex);
            }
        }

        // Update aluno 
        public async Task UpdateAluno(Aluno aluno)
        {
            {
                try
                {
                    _context.Entry(aluno).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    // Trata exceção do Entity Framework
                    throw new Exception("Error updating student in the database.", ex);
                }
                catch (Exception ex)
                {
                    // Trata outras exceções
                    throw new Exception("Error updating student", ex);
                }
            }
        }
        

        public async Task DeleteAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                throw new Exception("error to Deleting student in the database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error to deleting student", ex);
            }

        }


    }
}
