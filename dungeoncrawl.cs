using System;

namespace DungeonCrawl {

	 class Program {


		public static void Main(string[] args) {
			Console.ForegroundColor = ConsoleColor.Blue;
		
			Player player = new Player();
			
			Console.WriteLine("Welcome. Type your name and hit enter:");
			player.name = Console.ReadLine();
			
			Console.WriteLine("What species are you?\n");
			Console.WriteLine("Type: 1 for Archer, 2 for Mage, 3 for Warrior");
		
			int chosenRole = Convert.ToInt32(Console.ReadLine());
			while  ((chosenRole != 1) && (chosenRole != 2) && (chosenRole != 3)) {
				Console.WriteLine("The entry: " + chosenRole + " is not a valid input. Pick 1, 2 or 3.\n");
			        Console.WriteLine("What species are you?\n");
                                Console.WriteLine("Type: 1 for Archer, 2 for Mage, 3 for Warrior");
				chosenRole = Convert.ToInt32(Console.ReadLine());
			}

			player.role = chosenRole;
			
			Console.WriteLine("Welcome to the dungeon, " + player.name + ". You are now playing as a " + player.getRole() + "!");

			Location location = new Location(player, 0);
			
			if (player.health > 0) {
				player.health = 1000;
				Location location2 = new Location(player, 1);
			}
			else {
				return;
			}

			 if (player.health > 0) {
                                player.health = 1000;
                                Location location3 = new Location(player, 2);
                        }
                        else {  
                                return;
                        }
			
			if(player.health > 0) {
				   Console.WriteLine("You beat the dungeon!");
			}
			else {
				return;
			}
		}

                // *****************************************************************************************

		public class Living {
                        public string name { get; set; }
		        public int health { get; set; }
		}

                // *****************************************************************************************		

		// Player is now a child class, or derived class, from the Living class
		public class Player : Living {
			//   property where we can get a value and set a value 
	 	        public int role { get; set; }
			
			// constructor			
			public Player() {
				this.health = 1000;
			}

			public string getRole() {
				if (role == 1) {
					return "Archer";
				}
				else if (role == 2) {
					return "Mage";
				}
				else {
					return "Warrior";
				}
			}

			public void playerInfo() {
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("{0} {1} - {2} health left!", getRole(), this.name, this.health);
			}
			
		}

                // *****************************************************************************************

		public class Monster : Living {
			
			public Monster(string name, int health) {
				this.name = name;
				this.health = health;
			}
			
			public void monsterInfo() {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("{0} - {1} health left!", this.name, this.health);
			}
		}

	        // *****************************************************************************************

		public class Location {
			public Player player { get; set; }
			public string location { get; set; }
			public int depth { get; set; }

			  public string welcomeMessage(string location) {
                                return this.player.name + " has entered the " + location + "\n";
                        }	

			// constructor
			public Location(Player player, int depth) {
				this.player = player;
				this.depth = depth;

				// levels of depth
				if (this.depth == 0) {
					this.location = "Underground Realm";
					Console.WriteLine(welcomeMessage(this.location));

					Monster caveSpider = new Monster("Cave Spider", 500);
					Battle battle0 = new Battle(player, caveSpider);
				}	
				else if (this.depth == 1) {	
					this.location = "Cavern";
					Console.WriteLine(welcomeMessage(this.location));

					Monster creeper = new Monster("Creeper", 1000);
					Battle battle1 = new Battle(player, creeper);
				}
				else if (this.depth == 2) {
					this.location = "Underworld";
					Console.WriteLine(welcomeMessage(this.location));

					Monster enderdragon = new Monster("Enderdragon", 1500);
					Battle battle2 = new Battle(player, enderdragon);
				}
			}
		}

		// *****************************************************************************************

		public class Battle {
			
			public Player player { get; set; }
			public Monster monster { get; set; }
			public int playerAttack { get; set; } 

			public Battle(Player player, Monster monster) {
				this.player = player;
				this.monster = monster;
				// allow player to start battle
				bool turn = true;

				Console.WriteLine("A {0} has appeared! Get ready to battle...", this.monster.name);
				
				while(this.player.health > 0 && this.monster.health > 0) {
					Console.ForegroundColor = ConsoleColor.Blue;
					// your turn
					if (turn) {
						Console.WriteLine("\n-------------------------------");
						Console.WriteLine("It's your turn. What do you want to do?");
						if (this.player.role == 1) {
							Console.WriteLine("1 - Shoot an arrow. 2 - Attack with your sword.");
						}
						else if (this.player.role == 2) {
							Console.WriteLine("1 - Fire blast. 2 - Attack with your sword.");
						}
						else {
							Console.WriteLine("1 - Hit them with your hammer. 2 - Attack with your sword.");
						}

						int attackChoice = Convert.ToInt32(Console.ReadLine());
						int attackDmg = attackDamage(attackChoice);
						Console.WriteLine("\nThe " + this.monster.name + " took " + attackDmg + " HP in damage.");

						if (this.monster.health <= attackDmg) {
							this.monster.health = 0;
							Console.WriteLine("SUCCESS!!! You killed the " + this.monster.name + "!!!");
							break;
						}
						else {
							this.monster.health -= attackDmg;
							monster.monsterInfo();
						}
						turn = false;
					}
					else {
						Console.WriteLine("\n-------------------------------");
                                                Console.WriteLine("The " + this.monster.name + " lunges at you!");

						Random rnd = new Random();
						int monsterDmg = rnd.Next(0, 75);
						if (this.player.health <= monsterDmg) {
							this.player.health = 0;
                                                        Console.WriteLine("Oh no...the " + this.monster.name + " killed you.");
							break;
                                                }
                                                else {
                                                        this.player.health -= monsterDmg;
							Console.WriteLine("\nYou took " + monsterDmg + " HP in damage.");
                                                        player.playerInfo();
                                                }
						turn = true;
					}
				}
			}
			
			public int attackDamage(int attackChoice) {
				Random rnd = new Random();

				if(attackChoice == 1) {
					// speciality attacks can do a lot or a little, risky
					return (rnd.Next(0,101));
				}
				else {
					// sword attacks are safer choices
					return (rnd.Next(45,66));
				}
			}

		} 		
	}		
}
