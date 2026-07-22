namespace PointOfSale.Application.DTOs.Request.Login;

public record ResetPasswordRequest(int UserId, string NewPassword);