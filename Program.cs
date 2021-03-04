using System;
using System.Collections.Generic;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorioSerie = new SerieRepositorio();
        static FilmeRepositorio repositorioFilme = new FilmeRepositorio();

        

        static string tituloTipo = "";
        static void Main(string[] args)
        {
            escolhertipo();
         
       
            

            
            string opcaoUsuario = ObterOpcaoUsuario(tituloTipo);

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        Listar();
                        break;
                    case "2":
                        Inserir();
                        break;
                    case "3":
                        Atualizar();
                        break;
                    case "4":
                        Excluir();
                        break;
                    case "5":
                        Visualizar();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    case "O":
                        escolhertipo();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }   
                opcaoUsuario = ObterOpcaoUsuario(tituloTipo);
            }

           Console.WriteLine("Obrigado por utilizar nossos serviços!");
           Console.ReadLine();
            

        }

        public static string escolhertipo()
        {
            var sair = false;
            var escolha = "";
            do
            {
                Console.WriteLine("Escolha sua preferência Séries ou Filmes (S/F)");
                escolha = (Console.ReadLine()).ToUpper();
                if (escolha == "S" && escolha == "F")
                    sair = true;

            } while (sair);

            return tituloTipo = (escolha == "S" ? "Série" : "filme");
        }

        private static int digiteId(){
            
             Console.WriteLine($"Digite o id da {tituloTipo}");
            int indice = int.Parse(Console.ReadLine());
            return indice;
        }

        private static int digiteGenero(){  
            foreach (var i in Enum.GetValues(typeof(Genero)))
           {
               Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
           }

           Console.Write("Digite o gênero entre as opções acima: ");
           int entradaGenero = int.Parse(Console.ReadLine());

           return entradaGenero;
        }

        public static Tuple<string, int, string> preenchaDetalhes(){


           Console.Write($"Digite o Titulo da {tituloTipo}: ");
           string entradaTitulo = Console.ReadLine();
           
           Console.Write($"Digite o Ano de Início da {tituloTipo}: ");
           int entradaAno = int.Parse(Console.ReadLine());
           
           Console.Write($"Digite a Descrição da {tituloTipo}: ");
           string entradaDescricao = Console.ReadLine();
        
            return Tuple.Create(entradaTitulo, entradaAno, entradaDescricao);
        }


        

        private static void Visualizar()
        {
          
            if(tituloTipo == "filme"){
                var serie = repositorioFilme.RetornaPorId(digiteId());
                Console.WriteLine(nameof(tituloTipo));

                Console.WriteLine(serie);
            }else{
                var serie = repositorioSerie.RetornaPorId(digiteId());
                Console.WriteLine(serie);
            }

        }

        private static void Excluir()
        {
           var indiceSerie = digiteId();

          var serie = new object();

           if(tituloTipo == "filme"){
                 serie = repositorioFilme.RetornaPorId(digiteId());
                
            }else{
                 serie = repositorioSerie.RetornaPorId(digiteId());
            }

                Console.WriteLine(serie);
            //var serie = repositorio.RetornaPorId(indiceSerie);

            if (serie != null)
            {

                Console.WriteLine(serie);

                Console.WriteLine($"Confirma a exclusão da {tituloTipo} (S/N)?");

                string reposta = (Console.ReadLine()).ToUpper();

                if (reposta == "S"){
                    if(tituloTipo == "filme"){
                        repositorioFilme.Exclui(indiceSerie);
                
                    }else{
                        repositorioSerie.Exclui(indiceSerie);
                    }
                    
                    Console.WriteLine($"Exclusão da {tituloTipo} feita com sucesso!");
                }

            }

        }

        private static void Atualizar()
        {
            
            int indice = digiteId();

           int entradaGenero = digiteGenero();
           
            var entradas = preenchaDetalhes();

           
            if(tituloTipo == "filme"){
                Filme atualizaSerie = new Filme(id: indice,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradas.Item1,
                                        ano: entradas.Item2,
                                        descricao: entradas.Item3);

                repositorioFilme.Atualiza(indice, atualizaSerie);
                
            }else{
                Serie atualizaSerie = new Serie(id: indice,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradas.Item1,
                                        ano: entradas.Item2,
                                        descricao: entradas.Item3);
                repositorioSerie.Atualiza(indice, atualizaSerie);
            }
            
            
        }

        private static void Inserir()
        {
           Console.WriteLine("Inserir nova serie");

           int entradaGenero = digiteGenero();

           var entradas = preenchaDetalhes();
        
            if(tituloTipo == "filme"){
                Filme novo = new Filme(id: repositorioFilme.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradas.Item1,
                                        ano: entradas.Item2,
                                        descricao: entradas.Item3);

                repositorioFilme.Insere(novo);
                
            }else{
                Serie novo = new Serie(id: repositorioSerie.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradas.Item1,
                                        ano: entradas.Item2,
                                        descricao: entradas.Item3);
                repositorioSerie.Insere(novo);
            }
        }


     



        private static void Listar()
        {
            Console.WriteLine("Listar Series");
            
       

            if(tituloTipo == "filme"){
                List<Filme> lista = new List<Filme>();
               lista = repositorioFilme.Lista();
                if(lista.Count == 0){
                    Console.WriteLine($"Nenhum(a) {tituloTipo} cadastrada.");
                    return;
                }
                foreach (var serie in lista)
                { 
                    var excluido = serie.retornaExcluido();
                
                        Console.WriteLine($"#ID {serie.retornaId()}: - {serie.retornaTitulo()} "+"{0}", excluido ? "*Excluido*" : "");
                }
                
            }else{
                List<Serie> lista = new List<Serie>();
               lista =  repositorioSerie.Lista();
                if(lista.Count == 0){
                    Console.WriteLine($"Nenhum(a) {tituloTipo} cadastrada.");
                    return;
                }
                foreach (var serie in lista)
                { 
                    var excluido = serie.retornaExcluido();
                
                        Console.WriteLine($"#ID {serie.retornaId()}: - {serie.retornaTitulo()} "+"{0}", excluido ? "*Excluido*" : "");
                }
            }



            
        }

        private static string ObterOpcaoUsuario(string tipo){
            var trocaTitulo = "";
            if(tipo == "Filme")
            {
                trocaTitulo = "Série";
            }else{
                trocaTitulo = "Filme";
            }
            Console.WriteLine();

            Console.WriteLine($"DIO {tipo}s a seu dispor!!!");
            Console.WriteLine($"Informe a opção desejada:");

            Console.WriteLine($"1- Listar {tipo}s");
            Console.WriteLine($"2- Inserir nova(o) {tipo}" );
            Console.WriteLine($"3- Atualizar {tipo}");
            Console.WriteLine($"4- Excluir {tipo}");
            Console.WriteLine($"5- Visualizar {tipo}");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine($"O- Mudar para {trocaTitulo}");
            Console.WriteLine("X- Sair");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;

        }

        
    }
}
