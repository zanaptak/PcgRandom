#define XX_PREDECLS

#define XX_INFO \
            "pcg_mcg_32_xsh_rs_16_random_r:\n" \
            "      -  result:      16-bit unsigned int (uint16_t)\n" \
            "      -  period:      2^30\n" \
            "      -  state type:  struct pcg_state_32 (%zu bytes)\n" \
            "      -  output func: XSH-RS\n" \
            "\n", sizeof(struct pcg_state_32)

#define XX_NUMBITS                  "  16bit:"
#define XX_NUMVALUES                10
#define XX_NUMWRAP                  10
#define XX_PRINT_RNGVAL(value)      printf(" 0x%04x", value)
#define XX_RAND_DECL                struct pcg_state_32 rng;
#define XX_SEEDSDECL(seeds)         uint32_t seeds[1];
#define XX_SRANDOM_SEEDARGS(seeds)  seeds[0]
#define XX_SRANDOM_SEEDCONSTS       4294967295u
#define XX_SRANDOM(...)             \
            pcg_mcg_32_srandom_r(&rng, __VA_ARGS__)
#define XX_RANDOM()                 \
            pcg_mcg_32_xsh_rs_16_random_r(&rng)
#define XX_BOUNDEDRAND(bound)       \
            pcg_mcg_32_xsh_rs_16_boundedrand_r(&rng, bound)
#define XX_ADVANCE(delta)           \
            pcg_mcg_32_advance_r(&rng, delta)

#include "pcg_variants.h"
#include "check-base.c"
