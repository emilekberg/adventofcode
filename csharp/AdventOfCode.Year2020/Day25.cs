using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

public class Day25 : BaseDay<string[], long>, IDay
{
    public const int Divider = 20201227;
    public const int SubjectNumber = 7;
    public override long Part1(string[] input)
    {
        var formattedInput = input.Select(int.Parse).ToArray();
        var cardPublicKey = formattedInput[0];
        var doorPublicKey = formattedInput[1];

        return FindEncryptionKey(7, cardPublicKey, doorPublicKey);
    }
    /// <summary>
    /// Faster way to find encryption. Only calculates the cards encryption key and does no validation.
    /// </summary>
    /// <param name="subjectNumber"></param>
    /// <param name="publicKeyCard"></param>
    /// <param name="publicKeyDoor"></param>
    /// <returns></returns>
    public static long FindEncryptionKey(int subjectNumber, int publicKeyCard, int publicKeyDoor)
    {
        int secretLoopNumber = -1;
        var publicKey = 1;
        int i = 1;
        // Find the loop number for the card.
        do
        {
            publicKey = (publicKey * subjectNumber) % Divider;
            if (publicKey == publicKeyCard)
            {
                secretLoopNumber = i;
            }
            i++;
        }
        while (secretLoopNumber == -1);
        // calculates the encryption key for the card, by using the cards loop number and the doors public key.
        long encryptionKey = 1;
        for (i = 0; i < secretLoopNumber; i++)
        {
            encryptionKey = (encryptionKey * publicKeyDoor) % Divider;
        }
        return encryptionKey;
    }
    /// <summary>
    /// Validates the card and doors encryption key to make sure they are correct. A bit more expensive since it calculates both of them.
    /// </summary>
    /// <param name="publicKeyCard"></param>
    /// <param name="publicKeyDoor"></param>
    /// <returns></returns>
    public static long FindEncryptionKeyWithValidation(int publicKeyCard, int publicKeyDoor)
    {
        // find the looping number for the card and the door.
        var (card, door) = FindLoopNumbers(7, publicKeyCard, publicKeyDoor);

        // get the encryption key for both door and card.
        var encryptionKeyCard = TransformNumber(publicKeyDoor, card);
        var encryptionKeyDoor = TransformNumber(publicKeyCard, door);

        // validate that they are equal, which they should be.
        if (encryptionKeyCard != encryptionKeyDoor)
        {
            throw new ArgumentException($"card: {encryptionKeyCard} does not equal to door: {encryptionKeyDoor}");
        }
        return encryptionKeyCard;
    }

    /// <summary>
    /// Looks for both loop numbers in the same loop.
    /// </summary>
    /// <param name="subjectNumber"></param>
    /// <param name="expectedCard"></param>
    /// <param name="expectedDoor"></param>
    /// <returns></returns>
    public static (long card, long door) FindLoopNumbers(int subjectNumber, int expectedCard, int expectedDoor)
    {
        long cardSecretLoopNumber = -1;
        long doorSecretLoopNumber = -1;
        var publicKey = 1;
        int i = 1;
        do
        {
            publicKey = (publicKey * subjectNumber) % Divider;
            if (publicKey == expectedCard)
            {
                cardSecretLoopNumber = i;
            }
            if (publicKey == expectedDoor)
            {
                doorSecretLoopNumber = i;
            }
            i++;
        }
        while (cardSecretLoopNumber == -1 || doorSecretLoopNumber == -1);
        return (cardSecretLoopNumber, doorSecretLoopNumber);
    }

    /// <summary>
    /// Applies the transformation logic using the loopsize.
    /// </summary>
    /// <param name="subjectNumber"></param>
    /// <param name="loopSize"></param>
    /// <returns></returns>
    public static long TransformNumber(long subjectNumber, long loopSize)
    {
        long value = 1;
        for (int i = 0; i < loopSize; i++)
        {
            value = (value * subjectNumber) % Divider;
        }
        return value;
    }
}
