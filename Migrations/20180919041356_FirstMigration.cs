using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    address = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.UniqueConstraint("AK_AspNetUsers_user_id", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    created_at = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    created_at = table.Column<DateTime>(nullable: false),
                    desc = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    qty = table.Column<int>(nullable: false),
                    short_desc = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    message_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UsersId = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    message = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.message_id);
                    table.ForeignKey(
                        name: "FK_messages_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UsersId = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    sub_total = table.Column<double>(nullable: false),
                    tax = table.Column<double>(nullable: false),
                    total = table.Column<double>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_orders_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products_has_categories",
                columns: table => new
                {
                    products_categories_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    category_id = table.Column<int>(nullable: false),
                    product_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_has_categories", x => x.products_categories_id);
                    table.ForeignKey(
                        name: "FK_products_has_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_has_categories_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    comment_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UsersId = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    message_id = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_comments_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comments_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "message_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders_has_products",
                columns: table => new
                {
                    orders_products_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    order_id = table.Column<int>(nullable: false),
                    product_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders_has_products", x => x.orders_products_id);
                    table.ForeignKey(
                        name: "FK_orders_has_products_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_has_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_UsersId",
                table: "comments",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_message_id",
                table: "comments",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_UsersId",
                table: "messages",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UsersId",
                table: "orders",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_has_products_order_id",
                table: "orders_has_products",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_has_products_product_id",
                table: "orders_has_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_has_categories_category_id",
                table: "products_has_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_has_categories_product_id",
                table: "products_has_categories",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "orders_has_products");

            migrationBuilder.DropTable(
                name: "products_has_categories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
