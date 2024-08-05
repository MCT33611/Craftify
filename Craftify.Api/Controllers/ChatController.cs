using Craftify.Api.Contracts.Chat;
using Craftify.Application.Chat.Commands.AddMessageMedia;
using Craftify.Application.Chat.Commands.BlockUser;
using Craftify.Application.Chat.Commands.CreateConversation;
using Craftify.Application.Chat.Commands.DeleteMessage;
using Craftify.Application.Chat.Commands.DeleteMessageMedia;
using Craftify.Application.Chat.Commands.MarkConversationAsRead;
using Craftify.Application.Chat.Commands.SendMessage;
using Craftify.Application.Chat.Commands.UnblockUser;
using Craftify.Application.Chat.Commands.UpdateMessage;
using Craftify.Application.Chat.Common.Dtos;
using Craftify.Application.Chat.Common;
using Craftify.Application.Chat.Queries.GetConversationById;
using Craftify.Application.Chat.Queries.GetConversationsByUserId;
using Craftify.Application.Chat.Queries.GetLatestMessageByConversationId;
using Craftify.Application.Chat.Queries.GetMediaByMessageId;
using Craftify.Application.Chat.Queries.GetMediaByType;
using Craftify.Application.Chat.Queries.GetMessageById;
using Craftify.Application.Chat.Queries.GetMessagesByConversationId;
using Craftify.Application.Chat.Queries.GetPaginatedMessages;
using Craftify.Application.Chat.Queries.GetUnreadConversationsCount;
using Craftify.Application.Chat.Queries.GetUnreadMessagesCount;
using Craftify.Application.Chat.Queries.IsUserBlocked;
using Craftify.Application.Chat.Queries.SearchConversations;
using Craftify.Application.Chat.Queries.SearchMessages;
using Craftify.Domain.Common.Errors;
using Craftify.Domain.Enums;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;
using Craftify.Api.Hubs;
using Craftify.Application.Chat.Queries.GetConversationByRoomId;
using System.Security.Claims;

namespace Craftify.Api.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ChatController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("conversations")]
        public async Task<ActionResult<ConversationResult>> CreateConversation([FromBody] CreateConversationRequest request)
        {
            var command = new CreateConversationCommand
            {
                UserId1 = request.UserId1,
                UserId2 = request.UserId2
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("users/{blockedId}/block")]
        [Authorize]
        public async Task<ActionResult<bool>> BlockUser(Guid blockedId)
        {
            try
            {
                var blockerId = GetCurrentUserId();
                if (blockerId == null)
                {
                    return Unauthorized("User is not authenticated or user ID is invalid");
                }
                var command = new BlockUserCommand
                {
                    BlockerId = blockerId.Value,
                    BlockedId = blockedId
                };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("users/{unblockedId}/unblock")]
        [Authorize]
        public async Task<ActionResult<bool>> UnblockUser(Guid unblockedId)
        {
            try
            {
                var unblockerId = GetCurrentUserId();
                if (unblockerId == null)
                {
                    return Unauthorized("User is not authenticated or user ID is invalid");
                }
                var command = new UnblockUserCommand
                {
                    UnblockerId = unblockerId.Value,
                    UnblockedId = unblockedId
                };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("messages/{messageId}/media")]
        [Authorize]
        public async Task<ActionResult<MessageMediaResult>> AddMessageMedia(Guid messageId, [FromBody] AddMessageMediaRequest request)
        {
            try
            {
                var command = _mapper.Map<AddMessageMediaCommand>(request);
                command.MessageId = messageId;
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("messages/{messageId}/media/{mediaId}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteMessageMedia(Guid messageId, Guid mediaId)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated or user ID is invalid");
                }
                var command = new DeleteMessageMediaCommand
                {
                    MediaId = mediaId,
                    UserId = userId.Value
                };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("conversations/{conversationId}")]
        [Authorize]
        public async Task<ActionResult<ConversationResult>> GetConversationById(Guid conversationId)
        {
            try
            {
                var query = new GetConversationByIdQuery { ConversationId = conversationId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("conversations/room/{roomId}")]
        [Authorize]
        public async Task<ActionResult<ConversationResult>> GetConversationByRoomId(string RoomId)
        {
            try
            {
                var query = new GetConversationByRoomIdQuery { RoomId = RoomId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("conversations")]
        [Authorize]
        public async Task<ActionResult<List<ConversationResult>>> GetConversationsByUserId()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated or user ID is invalid");
                }
                var query = new GetConversationsByUserIdQuery { UserId = userId.Value };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("conversations/{conversationId}/messages")]
        [Authorize]
        public async Task<ActionResult<MessageListResult>> GetMessagesByConversationId(Guid conversationId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                var query = new GetMessagesByConversationIdQuery
                {
                    ConversationId = conversationId,
                    Page = page,
                    PageSize = pageSize
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("messages/{messageId}")]
        [Authorize]
        public async Task<ActionResult<MessageResult>> GetMessageById(Guid messageId)
        {
            try
            {
                var query = new GetMessageByIdQuery { MessageId = messageId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("unread-conversations-count")]
        [Authorize]
        public async Task<ActionResult<int>> GetUnreadConversationsCount()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated or user ID is invalid");
                }
                var query = new GetUnreadConversationsCountQuery { UserId = userId.Value };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("conversations/{conversationId}/latest-message")]
        [Authorize]
        public async Task<ActionResult<MessageResult>> GetLatestMessageByConversationId(Guid conversationId)
        {
            try
            {
                var query = new GetLatestMessageByConversationIdQuery(conversationId);
                var result = await _mediator.Send(query);

                if (result == null)
                {
                    return NotFound("No messages found for the given conversation.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("conversations/{conversationId}/unread-count")]
        [Authorize]
        public async Task<ActionResult<int>> GetUnreadMessagesCount(Guid conversationId)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated or user ID is invalid");
                }
                var query = new GetUnreadMessagesCountQuery(conversationId, userId.Value);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("conversations/{conversationId}/messages/paginated")]
        [Authorize]
        public async Task<ActionResult<PaginatedMessageResult>> GetPaginatedMessages(Guid conversationId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                var query = new GetPaginatedMessagesQuery(conversationId, page, pageSize);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("messages/{messageId}/media")]
        [Authorize]
        public async Task<ActionResult<List<MessageMediaResult>>> GetMediaByMessageId(Guid messageId)
        {
            try
            {
                var query = new GetMediaByMessageIdQuery(messageId);
                var result = await _mediator.Send(query);

                if (result == null || !result.Any())
                {
                    return NotFound("No media found for the given message.");
                }

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("conversations/{conversationId}/media/{mediaType}")]
        [Authorize]
        public async Task<ActionResult<List<MessageMediaResult>>> GetMediaByType(Guid conversationId, MediaType mediaType)
        {
            try
            {
                var query = new GetMediaByTypeQuery(conversationId, mediaType);
                var result = await _mediator.Send(query);

                if (result == null || !result.Any())
                {
                    return NotFound($"No {mediaType} media found for the given conversation.");
                }

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users/{otherUserId}/is-blocked")]
        [Authorize]
        public async Task<ActionResult<bool>> IsUserBlocked(Guid otherUserId)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                if (currentUserId == null)
                {
                    return Unauthorized("User is not authenticated or user ID is invalid");
                }
                var query = new IsUserBlockedQuery(currentUserId.Value, otherUserId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private Guid? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("uid");

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            return null;
        }
    }
}