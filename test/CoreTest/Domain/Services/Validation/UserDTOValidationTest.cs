using Core.Domain.Services.Validation;
using FluentValidation.Results;
using Shared;
using Shared.ValueObject.DTO;
using TestUtils.ValueObject;

namespace CoreTest.Domain.Services.Validation;

public class UserDTOValidationTest
{
    [Fact]
    public void ShouldNotHaveErrorWhenFieldsAreValid()
    {
        //Setup
        UserDTO userDTO = UserDTOTest.Build();

        //Execution
        UserDTOValidation validation = new UserDTOValidation();
        ValidationResult result = validation.Validate(userDTO);
        
        //Assertion
        Assert.True(result.IsValid);
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsBlank()
    {
        //Setup
        UserDTO userDTO = UserDTOTest.Build();
        userDTO.Name = string.Empty;
        
        //Execution
        UserDTOValidation validation = new UserDTOValidation();
        ValidationResult result = validation.Validate(userDTO);
        
        //Assertion
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count == 1);
        Assert.Equal(ErrorMessagesResource.NAME_BLANK, result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void ShouldHaveErrorWhenEmailIsBlank()
    {
        //Setup
        UserDTO userDTO = UserDTOTest.Build();
        userDTO.Email = string.Empty;
        
        //Execution
        UserDTOValidation validation = new UserDTOValidation();
        ValidationResult result = validation.Validate(userDTO);
        
        //Assertion
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count == 1);
        Assert.Equal(ErrorMessagesResource.EMAIL_BLANK, result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void ShouldHaveErrorWhenPasswordIsBlank()
    {
        //Setup
        UserDTO userDTO = UserDTOTest.Build();
        userDTO.Password = string.Empty;
        
        //Execution
        UserDTOValidation validation = new UserDTOValidation();
        ValidationResult result = validation.Validate(userDTO);
        
        //Assertion
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count == 1);
        Assert.Equal(ErrorMessagesResource.PASSWORD_BLANK, result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void ShouldHaveErrorWhenEmailIsInvalid()
    {
        //Setup
        UserDTO userDTO = UserDTOTest.Build();
        userDTO.Email = "invalid_email";
        
        //Execution
        UserDTOValidation validation = new UserDTOValidation();
        ValidationResult result = validation.Validate(userDTO);
        
        //Assertion
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count == 1);
        Assert.Equal(ErrorMessagesResource.EMAIL_INVALID, result.Errors[0].ErrorMessage);
    }
}
