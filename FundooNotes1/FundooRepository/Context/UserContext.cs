// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserContext.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Context
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using global::FundooModel;

    /// <summary>
    /// UserContext class responsible for database operations
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class
        /// </summary>
        /// <param name="options">DatabaseContextOptions option</param>
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Gets or sets DatabaseSet for users to manipulate user data
        /// </summary>
        public DbSet<UserModel> Users { get; set; }

        /// <summary>
        ///  Gets or sets DatabaseSet for notes to manipulate user data
        /// </summary>
        public DbSet<NotesModel> Notes { get; set; }

        /// <summary>
        ///  Gets or sets DatabaseSet for Collaborators to manipulate user data
        /// </summary>
        public DbSet<CollaboratorModel> Collaborators { get; set; }

        /// <summary>
        /// Gets or sets DatabaseSet for labels to manipulate user data
        /// </summary>
        public DbSet<LabelModel> Labels { get; set; }
    }
}
