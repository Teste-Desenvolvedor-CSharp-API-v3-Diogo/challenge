using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Questao1.Application.Commands;
using Questao1.Application.Handlers;
using System;
using System.Globalization;

namespace Questao1 {
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IBankAccountRepository, MockableBankAccountRepository>()
                .AddTransient<CreateBankAccountHandler>()
                .AddTransient<DepositToAccountHandler>()
                .AddTransient<WithdrawFromAccountHandler>()
                .AddTransient<ListAccountsHandler>()
                .BuildServiceProvider();

            var accountRepository = serviceProvider.GetService<IBankAccountRepository>();
            var createAccountHandler = serviceProvider.GetService<CreateBankAccountHandler>();
            var depositHandler = serviceProvider.GetService<DepositToAccountHandler>();
            var withdrawHandler = serviceProvider.GetService<WithdrawFromAccountHandler>();
            var listAccountsHandler = serviceProvider.GetService<ListAccountsHandler>();

            while (true)
            {
                Console.WriteLine("\nSelecione uma opção:");
                Console.WriteLine("1 - Cadastrar nova conta");
                Console.WriteLine("2 - Visualizar contas cadastradas");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");
                string opcao = Console.ReadLine();

                if (opcao == "0") break;

                switch (opcao)
                {
                    case "1":
                        Console.Write("Entre o número da conta: ");
                        int numero = int.Parse(Console.ReadLine());
                        Console.Write("Entre o titular da conta: ");
                        string titular = Console.ReadLine();
                        Console.Write("Haverá depósito inicial (s/n)? ");
                        char resp = char.Parse(Console.ReadLine());

                        decimal depositoInicial = 0;
                        if (resp == 's' || resp == 'S')
                        {
                            Console.Write("Entre o valor de depósito inicial: ");
                            depositoInicial = decimal.Parse(Console.ReadLine());
                        }

                        var createAccountCommand = new CreateBankAccountCommand(numero, titular, depositoInicial);
                        var account = createAccountHandler.Handle(createAccountCommand);

                        Console.WriteLine("\nDados da conta:");
                        Console.WriteLine(account);

                        Console.Write("\nEntre um valor para depósito: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        var depositCommand = new DepositToAccountCommand(numero, depositAmount);
                        account = depositHandler.Handle(depositCommand);
                        Console.WriteLine("Dados da conta atualizados:");
                        Console.WriteLine(account);

                        Console.Write("\nEntre um valor para saque: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        var withdrawCommand = new WithdrawFromAccountCommand(numero, withdrawAmount);
                        account = withdrawHandler.Handle(withdrawCommand);
                        Console.WriteLine("Dados da conta atualizados:");
                        Console.WriteLine(account);
                        break;

                    case "2":
                        Console.WriteLine("\nContas cadastradas:");
                        var listAccountsCommand = new ListAccountsCommand();
                        var accounts = listAccountsHandler.Handle(listAccountsCommand);
                        foreach (var item in accounts)
                        {
                            Console.WriteLine(item);
                        }
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
