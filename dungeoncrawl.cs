using System;

namespace DungeonCrawl {

	 class Program {


		public static void Main(string[] args) {
			Player player = new Player();
			
			Console.WriteLine("Welcome. Type your name and hit enter:");
			player.name = Console.ReadLine();
			
			Console.WriteLine("What species are you?\n");
			Console.WriteLine("Type: 1 for Archer, 2 for Mage, 3 for Warrior");
		
			int chosenRole = Convert.ToInt32(Console.ReadLine());
			while  ((chosenRole != 1) && (chosenRole != 2) && (chosenRole != 3)) {
				Console.WriteLine("\n-------------------------------");
				Console.WriteLine("The entry: " + chosenRole + " is not a valid input. Pick 1, 2 or 3.\n");
			        Console.WriteLine("What species are you?\n");
                                Console.WriteLine("Type: 1 for Archer, 2 for Mage, 3 for Warrior");
				chosenRole = Convert.ToInt32(Console.ReadLine());
			}

			player.role = chosenRole;
			
			Console.WriteLine("Welcome to the dungeon, " + player.name + ". You are now playing as a " + player.getRole() + "!");

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
				this.health = 10;
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

			public string playerInfo() {
				return String.Format("{0} {1} - {2} health left!", getRole(), this.name, this.health);
			}
			
		}

                // *****************************************************************************************

		public class Monster : Living {
			
			public Monster(string name, int health) {
				this.name = name;
				this.health = health;
			}
			
			public string monsterInfo() {
				return String.Format("{0} with {1} health left!", this.name, this.health);
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

					Monster caveSpider = new Monster("Cave Spider", 5);
					Battle battle0 = new Battle(player, caveSpider);
				}	
				else if (this.depth == 1) {	
					this.location = "Cavern";
					Console.WriteLine(welcomeMessage(this.location));

					Monster creeper = new Monster("Creeper", 10);
					Battle battle1 = new Battle(player, creeper);
				}
				else if (this.depth == 2) {
					this.location = "Underworld";
					Console.WriteLine(welcomeMessage(this.location));

					Monster enderdragon = new Monster("Enderdragon", 15);
					Battle battle2 = new Battle(player, enderdragon);
				}
			}
		}

		// *****************************************************************************************

		public class Battle {
			
			public Player player { get; set; }
			public Monster monster { get; set; }
			public bool yourTurn { get; set; }
			public int playerAttack { get; set; } 

			public Battle(Player player, Monster monster) {
				this.player = player;
				this.monster = monster;
				// allow player to start battle
				//this.turn = true;

				Console.WriteLine("A {0} has appeared! Get ready to battle...", this.monster.name);
				
				// Battle loop
			}
			public void endMessage (bool end) {
				if(end) {
					Console.WriteLine("You have defeated {0}!\n", this.monster.name);
				}
				else {
					Console.WriteLine("You have defeated {0}! You beat the dungeon!", this.monster.name);
				}
			}
		} 		
	}		
}
