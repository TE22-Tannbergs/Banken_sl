/*
int maxMoney = 100;
bool login = true;
string correctPassword1 = "0000";
string correctPassword2 = "1111";
int passwordFail = 0;
string currentUser = "";
bool bankloop = true;
int money = 0;
string userFile = "";
 
void LoadUserFile(){
    if (currentUser == "user1"){
        userFile = "user1Money.txt";
    } else if (currentUser == "user2"){
        userFile = "user2Money.txt";
    }
}
 
try {
    LoadUserFile();
 
    using (StreamReader reader = new (userFile)) {
        money = int.Parse(reader.ReadToEnd());
    }
} catch {
    Console.WriteLine(" ");
}
 
bool verifyPassword1(string password){
  currentUser = "user1";
  return correctPassword1 == password;
  }
bool verifyPassword2(string password){
  currentUser = "user2";
  return correctPassword2 == password;
  }
 
while (login){
  if (passwordFail > 3){
    Console.WriteLine(" To many incorrect tries, exeting Tann-banken.\n");
    Environment.Exit(0);
  }
Console.Write(" What is the password: ");
string passwordAttempt = Console.ReadLine();
 
if (verifyPassword1(passwordAttempt)){
    Console.WriteLine(" Correct, welcome Samuel to Tann-banken\n");
    login = false;
    LoadUserFile();
 
    try {
        using (StreamReader reader = new StreamReader(userFile)) {
            money = int.Parse(reader.ReadToEnd());
        }
    } catch {
        Console.WriteLine("File was not reached");
        money = 0;
    }
    Console.WriteLine("Press ENTER to continue");
    Console.ReadKey();
 
} else if (verifyPassword2(passwordAttempt)){
  Console.WriteLine(" Correct, welcome Tommy to Tann-banken\n");
  login = false;
  LoadUserFile();
 
  try {
    using (StreamReader reader = new StreamReader(userFile)) {
        money = int.Parse(reader.ReadToEnd());
    }
  } catch {
    Console.WriteLine("File was not reached");
    money = 0;
  }
  Console.WriteLine("Press ENTER to continue");
  Console.ReadKey();
 
} else{
Console.WriteLine(" Incorrect password, try again.\n");
passwordFail ++;
    }
}
 
void deposit(){
  Console.WriteLine(" How much do you want to deposit");
    Console.WriteLine($" Balance: {money}");
  int deposition = int.Parse(Console.ReadLine());
  if (deposition <= maxMoney){
        Console.WriteLine($"You deposited {deposition}");
        Console.WriteLine($"New balance: {deposition + money}\n");
        money += deposition;
      } else {
        Console.WriteLine(" Insuficient balance");
      }
      Console.WriteLine("Press ENTER to return");
      Console.ReadKey();
}
 
void withdraw(){
  Console.WriteLine(" How much do you want to withdraw");
    Console.WriteLine($" Balance: {money}");
  int moneyWithdrawn = int.Parse(Console.ReadLine());
  if (moneyWithdrawn > money){
    Console.WriteLine(" Insuficient balance\n");
    } else if (moneyWithdrawn > 100){
      Console.WriteLine("Insuficient balance");
    }
    else {
    Console.WriteLine($" You have withdrawn: {moneyWithdrawn}");
    Console.WriteLine($" New balance: {money - moneyWithdrawn}\n");
    money -= moneyWithdrawn;
  };
  Console.WriteLine("Press ENTER to return");
  Console.ReadKey();
}
 
void checkBalance(){
  Console.WriteLine($" You have {money} left");
  Console.WriteLine("Press ENTER to return");
  Console.ReadKey();
}
void exitBank(){
    Console.WriteLine(" Thank you for using tann-bank");
    bankloop = false;
}
 
 void inBank(){
  while (bankloop){
  Console.Clear();
  Console.WriteLine("1. Deposit");
  Console.WriteLine("2. Withdrawl");
  Console.WriteLine("3. Balance");
  Console.WriteLine("4. Exit");
  switch(Console.ReadKey().KeyChar){
    case '1':
    deposit();
    break;
    case '2':
    withdraw();
    break;
    case '3':
    checkBalance();
    break;
    case'4':
    exitBank();
    break;
    }
  }
}
 
LoadUserFile();
 
switch (currentUser){
  case "user1":
  inBank();
  break;
  case "user2":
  inBank();
  break;
}
 
using (StreamWriter outputFile = new StreamWriter(userFile)) {
    outputFile.WriteLine(money);
}
*/


using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;

class BankAccount {
    public int Balance {get; private set;}
    public string UserFile {get;}

    public BankAccount(string userFile) {
        UserFile = userFile;
        LoadBalance();
    }

    private void LoadBalance() {
        try {
            using (StreamReader reader = new StreamReader(UserFile)) {
                Balance = int.Parse(reader.ReadToEnd());
            }
        } catch {
            Console.WriteLine("File could not be read. Defaulting balance to 100.");
            Balance = 100;
        }
    }

    public void SaveBalance() {
        using (StreamWriter writer = new StreamWriter(UserFile)) {
            writer.WriteLine(Balance);
        }
    }

    public void Deposit(int amount) {
        Balance += amount;
    }

    public bool Withdraw(int amount) {
        if (amount <= Balance) {
            Balance -= amount;
            return true;
        }
        return false;
    }
}


class PasswordVerifier {
    private readonly string _password;
    private readonly string _username;

    public PasswordVerifier(string username, string password) {
        _username = username;
        _password = password;
    }

    public bool Verify(string passwordInput) {
        return _password == passwordInput;
    }

    public string GetUsername() {
        return _username;
    }
}

class User {
    public string Username { get; }
    private readonly PasswordVerifier _passwordVerifier;

    public User(string username, string password) {
        Username = username;
        _passwordVerifier = new PasswordVerifier(username, password);
    }

    public bool Authenticate(string passwordInput) {
        return _passwordVerifier.Verify(passwordInput);
    }
}

class BankOperations {
    private readonly BankAccount _account;
    private const int MaxMoney = 100;

    public BankOperations(BankAccount account) {
        _account = account;
    }

    public void Deposit() {
        Console.WriteLine("How much do you want to deposit?");
        Console.WriteLine($"Balance: {_account.Balance}");
        int depositAmount = int.Parse(Console.ReadLine());
        if (depositAmount <= MaxMoney) {
            _account.Deposit(depositAmount);
            Console.WriteLine($"You deposited {depositAmount}. New balance: {_account.Balance}");
        } else {
            Console.WriteLine("Deposit exceeds the maximum allowed amount.");
        }
        Pause();
    }

    public void Withdraw() {
        Console.WriteLine("How much do you want to withdraw?");
        Console.WriteLine($"Balance: {_account.Balance}");
        int withdrawalAmount = int.Parse(Console.ReadLine());
        if (_account.Withdraw(withdrawalAmount)) {
            Console.WriteLine($"You have withdrawn: {withdrawalAmount}. New balance: {_account.Balance}");
        } else {
            Console.WriteLine("Insufficient balance.");
        }
        Pause();
    }

    public void CheckBalance() {
        Console.WriteLine($"Your balance is {_account.Balance}.");
        Pause();
    }

    public void Exit() {
        Console.WriteLine("Thank you for using Tann-Banken.");
        _account.SaveBalance();
    }

    private void Pause() {
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadKey();
    }
}

class Program {
    static void Main() {
        User user1 = new User("user1", "0000");
        User user2 = new User("user2", "1111");

        bool login = true;
        int passwordFail = 0;
        User loggedInUser = null;

        
        while (login) {
            if (passwordFail > 3) {
                Console.WriteLine("Too many incorrect attempts. Exiting Tann-Banken.");
                return;
            }

            Console.Write("Enter your password: ");
            string passwordInput = Console.ReadLine();

            if (user1.Authenticate(passwordInput)) {
                Console.WriteLine("Correct! Welcome Samuel to Tann-Banken.");
                loggedInUser = user1;
                login = false;
            } else if (user2.Authenticate(passwordInput)) {
                Console.WriteLine("Correct! Welcome Tommy to Tann-Banken.");
                loggedInUser = user2;
                login = false;
            } else {
                Console.WriteLine("Incorrect password. Try again.");
                passwordFail++;
            }
        }

      
        BankAccount account = new BankAccount($"{loggedInUser.Username}Money.txt");
        BankOperations operations = new BankOperations(account);

        
        bool bankLoop = true;
        while (bankLoop) {
            Console.Clear();
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Balance");
            Console.WriteLine("4. Exit");

            switch (Console.ReadKey(true).KeyChar) {
                case '1':
                    operations.Deposit();
                    break;
                case '2':
                    operations.Withdraw();
                    break;
                case '3':
                    operations.CheckBalance();
                    break;
                case '4':
                    operations.Exit();
                    return;
                    break;
            }
        }
    }
}


