using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ProjetoIT;

namespace ProjetoIt
{
	class Program 
	{
		const string path = @"C:\Users\dudus\Desktop\projeto\ProjetoIT\bolsistas.csv";
		static void Main(string[]args)
		{	
			//lendo o arquivo :3
			
			var reader = new StreamReader(File.OpenRead(path));
			var line = reader.ReadLine();
			var columns = line.Split(";");
			(int indexName, int indexCpf, int indexEntEnsino, int indexAno, int indexVlrBolsa) = SetColumnsIndex(columns);
			string opcaoUsuario = OpcaoUsuario();
			var bolsistas = BuildPeopleList(reader, indexName, indexCpf, indexEntEnsino, indexAno, indexVlrBolsa);
			
			while(opcaoUsuario != "X")
			{
				switch (opcaoUsuario)
				 {
					 //achando o bolsista zero do ano desejado B)
					 
					case "1":
						Console.WriteLine("digite um ano (2013 - 2016): ");
						string anoDesejado = Console.ReadLine();
						var lista_ano = new List<BolsistasM>();
						foreach (var item in bolsistas)
						{
							if (item.ano == anoDesejado)
			   		 		{
				   				 lista_ano.Add(item);
							}
						}

						System.Console.WriteLine($"-Bolsista zero- nome: {lista_ano[0].nome}, cpf: {lista_ano[0].cpf}, entidade de ensino: {lista_ano[0].entidade_de_ensino}, valor da bolsa: {lista_ano[0].valor_da_bolsa} ");

						break;
					 //procurando nomes na lista e embaralhando *w*	
					case "2":
						System.Console.WriteLine("Digite um Nome:");
		   				var procuraNome = Console.ReadLine().ToUpper();
						var lista_nomes = new List<BolsistasM>();
						string nomeCodificado = null;
						foreach (var item in bolsistas)
						{
							if (item.nome.Contains(procuraNome))
							{
								lista_nomes.Add(item);
							}
						}
						for (int i = 0; i < lista_nomes.Count; i++)
						{
							System.Console.WriteLine($"[{i}]: {lista_nomes[i].nome}");
						}		
						System.Console.WriteLine();
						System.Console.WriteLine("Digite o index do nome desejado:");
						var indexListaNome = int.Parse(Console.ReadLine());
						if(indexListaNome <= lista_nomes.Count)
						{
							nomeCodificado = EmbaralhaNome(lista_nomes[indexListaNome].nome);
						}

						System.Console.WriteLine($"Nome codificado: {nomeCodificado}, Ano: {lista_nomes[indexListaNome].ano}, Ent. de ensino{lista_nomes[indexListaNome].entidade_de_ensino}, Valor da bolsa: {lista_nomes[indexListaNome].valor_da_bolsa}");
						break;
						
					case "3":
						Console.WriteLine("Digite um ano");
						string ano_Desejado = Console.ReadLine();
						List<int> media_ano = new List<int>();
						foreach (var item in bolsistas)
						{
							if (item.ano == ano_Desejado)
			   				{
								media_ano.Add(int.Parse(item.valor_da_bolsa));
								
							}
						}
						int media = (int)media_ano.Average();
						System.Console.WriteLine($"A média anual é: {media}");
						break;
						
					case "4":
						int index = 0;
						var queryASC = bolsistas.OrderBy(bolsista => int.Parse(bolsista.valor_da_bolsa));
						var queryDSC = bolsistas.OrderByDescending(bolsista => int.Parse(bolsista.valor_da_bolsa));
						foreach (var item in queryASC)
						{
							if(index < 3)
							{
								System.Console.WriteLine($"Nome: {item.nome}, Valor da bolsa: {item.valor_da_bolsa}");
							}
							index++;
						}
						foreach (var item in queryDSC)
						{
							if(index < 3)
							{
								System.Console.WriteLine($"Nome: {item.nome}, Valor da bolsa: {item.valor_da_bolsa}");
							}
							index++;
						}
						break;
						
					 default:
						 throw new ArgumentOutOfRangeException();
				 }
				opcaoUsuario = OpcaoUsuario();
			}
		}
		//embaralhando o nome do bolsita :v
		
		private static string EmbaralhaNome(string nome)
		{
			Random num = new Random();

			string randomiza = new string(nome.ToCharArray().
			OrderBy(s => (num.Next(2) % 2) == 0).ToArray());
			return randomiza;
		}
 
		
		//indexando colunas :)
		private static (int,int,int,int,int) SetColumnsIndex(string[] columns) 	
		{
		Console.WriteLine("Pegando as posições de cada coluna.");	
		int indexName = -1;
		int indexCpf = -1;
		int indexEntEnsino = -1;
		int indexAno = -1;
		int indexVlrBolsa = -1;
		for (int i = 0; i < columns.Length; i++)
		{
			if(string.IsNullOrEmpty(columns[i]))
				continue;
			
			if(columns[i].ToUpper() == "NM_BOLSISTA")
			
			{
				indexName = i;
			}
			
			if(columns[i].ToUpper() == "CPF_BOLSISTA")
			{
				indexCpf = i; 
			}
			if(columns[i].ToUpper() == "NM_ENTIDADE_ENSINO")
			{
				 indexEntEnsino= i; 
			}if(columns[i].ToUpper() == "AN_REFERENCIA")
			{
				 indexAno= i; 
			}if(columns[i].ToUpper() == "VL_BOLSISTA_PAGAMENTO")
			{
				indexVlrBolsa = i; 
			}
			
		}
		return(indexName, indexCpf, indexEntEnsino, indexAno, indexVlrBolsa); 
		}
		
		//Criando lista de bolsistas :P
		
		private static List<BolsistasM> BuildPeopleList(StreamReader reader,int indexName,int indexCpf,int indexEntEnsino,int indexAno,int indexVlrBolsa)
		{
			Console.WriteLine("Montando listas de pessoas");
			string line;
			var bolsistas = new List<BolsistasM>();
			BolsistasM bolsistasM;
			while((line = reader.ReadLine()) != null)
			{
				var values = line.Split(";");
				bolsistasM = new BolsistasM();
				
				if(indexName != -1)
					bolsistasM.nome = values[indexName];
					
				if(indexCpf != -1)
					bolsistasM.cpf = values[indexCpf];
					
				if(indexEntEnsino != -1)
					bolsistasM.entidade_de_ensino = values[indexEntEnsino];
					
				if(indexAno != -1)
					bolsistasM.ano = values[indexAno];
					
				if(indexVlrBolsa != -1)
					bolsistasM.valor_da_bolsa = values[indexVlrBolsa];
				
				bolsistas.Add(bolsistasM);
			
			}
			return bolsistas;
		}
		
		//Obtendo input do usuario XD
		
		private static string OpcaoUsuario()
		{
			System.Console.WriteLine("1. Buscar bolsista");
			System.Console.WriteLine("2. embaralhar nome");
			System.Console.WriteLine("3. Consultar média anual");
			System.Console.WriteLine("4. Ranking valores de bolsa");
			System.Console.WriteLine("X. Sair");

			System.Console.WriteLine();
			System.Console.WriteLine("Seu Input:");
			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
	}
}   
