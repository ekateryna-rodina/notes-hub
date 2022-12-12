using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNote.Dal.Migrations
{
    /// <inheritdoc />
    public partial class FieldsConstChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_CommentId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Notes_NoteId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UserProfiles_UserProfileId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Interaction_Comment_CommentId",
                table: "Interaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Interaction_Notes_NoteId",
                table: "Interaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Interaction_UserProfiles_UserProfileId",
                table: "Interaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Reference_ReferenceId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteTag_Tag_TagsId",
                table: "NoteTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reference",
                table: "Reference");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interaction",
                table: "Interaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Reference",
                newName: "References");

            migrationBuilder.RenameTable(
                name: "Interaction",
                newName: "Interactions");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Interaction_UserProfileId",
                table: "Interactions",
                newName: "IX_Interactions_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Interaction_NoteId",
                table: "Interactions",
                newName: "IX_Interactions_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Interaction_CommentId",
                table: "Interactions",
                newName: "IX_Interactions_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserProfileId",
                table: "Comments",
                newName: "IX_Comments_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_NoteId",
                table: "Comments",
                newName: "IX_Comments_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CommentId",
                table: "Comments",
                newName: "IX_Comments_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_References",
                table: "References",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interactions",
                table: "Interactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Notes_NoteId",
                table: "Comments",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserProfiles_UserProfileId",
                table: "Comments",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Comments_CommentId",
                table: "Interactions",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Notes_NoteId",
                table: "Interactions",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_UserProfiles_UserProfileId",
                table: "Interactions",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_References_ReferenceId",
                table: "Notes",
                column: "ReferenceId",
                principalTable: "References",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTag_Tags_TagsId",
                table: "NoteTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Notes_NoteId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserProfiles_UserProfileId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Comments_CommentId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Notes_NoteId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_UserProfiles_UserProfileId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_References_ReferenceId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteTag_Tags_TagsId",
                table: "NoteTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_References",
                table: "References");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interactions",
                table: "Interactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "References",
                newName: "Reference");

            migrationBuilder.RenameTable(
                name: "Interactions",
                newName: "Interaction");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Interactions_UserProfileId",
                table: "Interaction",
                newName: "IX_Interaction_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Interactions_NoteId",
                table: "Interaction",
                newName: "IX_Interaction_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Interactions_CommentId",
                table: "Interaction",
                newName: "IX_Interaction_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserProfileId",
                table: "Comment",
                newName: "IX_Comment_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_NoteId",
                table: "Comment",
                newName: "IX_Comment_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommentId",
                table: "Comment",
                newName: "IX_Comment_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reference",
                table: "Reference",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interaction",
                table: "Interaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_CommentId",
                table: "Comment",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Notes_NoteId",
                table: "Comment",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserProfiles_UserProfileId",
                table: "Comment",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interaction_Comment_CommentId",
                table: "Interaction",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interaction_Notes_NoteId",
                table: "Interaction",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interaction_UserProfiles_UserProfileId",
                table: "Interaction",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Reference_ReferenceId",
                table: "Notes",
                column: "ReferenceId",
                principalTable: "Reference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTag_Tag_TagsId",
                table: "NoteTag",
                column: "TagsId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
