using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class addmuscledataseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2c6891f3-98d9-4bfe-bf90-a7e5b6b1caf8"), "Three muscles located on the back of the thigh.", "Hamstrings (Back of Leg)" },
                    { new Guid("405069d0-6e66-4d50-883d-e5b29a403a84"), "Muscle extending from the neck to the mid-back.", "Trapezius (Middle Back)" },
                    { new Guid("619fe532-8226-4d84-9a61-b64bcdef8084"), "Largest muscle of the gluteal group, forming the buttocks.", "Gluteus Maximus (Butt)" },
                    { new Guid("6ed76f26-52f2-4350-b0b3-103e370f0835"), "Front upper arm muscle responsible for elbow flexion.", "Biceps" },
                    { new Guid("7be040bd-2759-4b13-a049-8cde648dfd75"), "Muscles of the forearm responsible for wrist and finger movements.", "Forearms" },
                    { new Guid("8909c9d4-2f45-4a2f-90c8-d6f475820a2f"), "Upper part of the pectoralis major muscle.", "Clavicular Head (Upper Chest)" },
                    { new Guid("a2528d27-0dfe-48c8-9a18-7887dd9743ef"), "Large muscles on the sides of the back.", "Latissimus Dorsi (Lats)" },
                    { new Guid("b391d0e8-442e-414a-85cc-9445b3bd11be"), "Four muscles located on the front of the thigh.", "Quadriceps (Front of Leg)" },
                    { new Guid("b61c068e-8e8b-4756-a05c-482d0ec70f9e"), "Muscles running along the spine, supporting posture.", "Erector Spinae (Lower Back)" },
                    { new Guid("c4ac8999-40a4-40c8-9518-42be7e2d0744"), "The back portion of the deltoid muscle.", "Posterior Deltoid (shoulder-Back)" },
                    { new Guid("cb5afad8-c1ae-44e5-ac7b-aa992f5c80a7"), "Muscle underneath the gastrocnemius, part of the calf.", "Soleus (Calves)" },
                    { new Guid("d729e9f8-3384-4873-83bc-74acdccacabe"), "The front portion of the deltoid muscle.", "Anterior Deltoid (shoulder-Front)" },
                    { new Guid("d8738439-5832-4764-8290-20ebe48d50dc"), "Lower part of the pectoralis major muscle.", "Sternal Head (Lower Chest)" },
                    { new Guid("df475ba8-6be7-42b6-871f-6a29cd4c91d8"), "Shoulder muscle composed of three heads: anterior, lateral, and posterior.", "Deltoid (shoulder-Top)" },
                    { new Guid("e56d075d-e1db-49d0-be1c-a925b80e87a6"), "The larger calf muscle, forming the bulge of the calf.", "Gastrocnemius (Calves)" },
                    { new Guid("f19a5492-81ea-4ebc-8b03-1198e8440a58"), "Back upper arm muscle responsible for elbow extension.", "Triceps" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("2c6891f3-98d9-4bfe-bf90-a7e5b6b1caf8"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("405069d0-6e66-4d50-883d-e5b29a403a84"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("619fe532-8226-4d84-9a61-b64bcdef8084"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("6ed76f26-52f2-4350-b0b3-103e370f0835"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("7be040bd-2759-4b13-a049-8cde648dfd75"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("8909c9d4-2f45-4a2f-90c8-d6f475820a2f"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("a2528d27-0dfe-48c8-9a18-7887dd9743ef"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("b391d0e8-442e-414a-85cc-9445b3bd11be"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("b61c068e-8e8b-4756-a05c-482d0ec70f9e"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("c4ac8999-40a4-40c8-9518-42be7e2d0744"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("cb5afad8-c1ae-44e5-ac7b-aa992f5c80a7"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("d729e9f8-3384-4873-83bc-74acdccacabe"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("d8738439-5832-4764-8290-20ebe48d50dc"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("df475ba8-6be7-42b6-871f-6a29cd4c91d8"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("e56d075d-e1db-49d0-be1c-a925b80e87a6"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("f19a5492-81ea-4ebc-8b03-1198e8440a58"));
        }
    }
}
