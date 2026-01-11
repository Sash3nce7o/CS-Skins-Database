// var context = new SkinsDbContext();
        // context.Database.Migrate();
        // Console.WriteLine("✅ Database created/updated");
        
        // var repo = new Repository(context);
        // var userService = new UserService(repo);
        // var skinService = new SkinService(repo);
        // var newUser = new UserRegisterViewModel{
        //     Username = "Balistic",
        //     Email = "balistic@skins.com",
        //     Password = "balls"
        // };


        
        // userService.Add(newUser);
        // Console.WriteLine("✅ User created: " + newUser.Username);

        // // Get the created user back
        // var user = context.Users.First();
        // Console.WriteLine($"📋 User ID: {user.Id}");


        // // Create a skin for this user
        // var newSkin = new SkinCreateViewModel
        // {
        //     Name = "AWP Dragon Lore",
        //     Float = 0.05f,
        //     Pattern = "Souvenir",
        //     MaxFloat = 0.08f,
        //     OwnerId = user.Id
        // };

        // skinService.Add(newSkin);
        // Console.WriteLine("✅ Skin created: " + newSkin.Name);

        // Get the created skin back
        // var skin = context.Skins.First();
        // Console.WriteLine($"📋 Skin ID: {skin.Id}, Quality: {skin.Quality}");

        // // Update user
        // var updateUser = new UserUpdateViewModel
        // {
        //     Username = "DragonMaster"
        // };

        // userService.Update(user.Id, updateUser);
        // Console.WriteLine("✅ User updated");
        
        
        // // Update skin
        // var updateSkin = new SkinUpdateViewModel
        // {
        //     Float = 0.06f,
        //     Pattern = "StatTrak"

        // };
        // skinService.Update(skin.Id, updateSkin);
        // Console.WriteLine("✅ Skin updated");
        
        
        // // Get updated values
        // var updatedUser = userService.GetById(user.Id);
        // var updatedSkin = skinService.GetById(skin.Id);

        // Console.WriteLine($"\n📊 Final State:");
        // Console.WriteLine($"User: {updatedUser.Username} ({updatedUser.Email})");
        // Console.WriteLine($"Skin: {updatedSkin.Name} - Float: {updatedSkin.Float}, Pattern: {updatedSkin.Pattern}");

        // // Delete skin
        // skinService.Remove(skin.Id);
        // Console.WriteLine("\n✅ Skin deleted");

        // Console.WriteLine("\n🎉 All operations completed successfully!");